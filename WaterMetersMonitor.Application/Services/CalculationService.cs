using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Application.Errors;
using WaterMetersMonitor.Application.Exceptions;
using WaterMetersMonitor.Domain.Entities;
using WaterMetersMonitor.Infrastructure.DataContexts;

namespace WaterMetersMonitor.Application.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly SqlDataContext _context;

        public CalculationService(SqlDataContext context)
        {
            _context = context;
        }


        public async Task AddRecord(long groupId, MainWaterMeterValue mainValue, List<WaterMeterValue> waterMeterValues)
        {
            var group = await _context.Set<Group>()
                .Include(e => e.MainWaterMeter)
                .ThenInclude(e => e.MainWaterMeterValues)
                .Include(e => e.Users)
                .ThenInclude(e => e.WaterMeters)
                .ThenInclude(e => e.WaterMeterValues)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }
            Dictionary<long, double> waterMeters = new Dictionary<long, double>();
            waterMeterValues.ForEach(e =>
            {
                waterMeters.Add(e.WaterMeterId, e.Value);
            });

            var time = DateTime.UtcNow;
            
            foreach(var user in group.Users)
            {
                foreach(var waterMeter in user.WaterMeters)
                {
                    if (waterMeters.TryGetValue(waterMeter.Id, out double value))
                    {
                        var waterMeterValue = new WaterMeterValue()
                        {
                            Value = value,
                            ValueTime = time,
                            WaterMeterId = waterMeter.Id
                        };
                        waterMeter.WaterMeterValues.Add(waterMeterValue);
                    }
                }
            }

            var mainWaterMeterValue = new MainWaterMeterValue()
            {
                Value = mainValue.Value,
                ValueTime = time,
                MainWaterMeterId = group.MainWaterMeter.Id
            };
            group.MainWaterMeter.MainWaterMeterValues.Add(mainWaterMeterValue);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.Conflict, RepositoryErrorCodes.ConstraintsNotMatched.ToString(), ex.Message);
            }
        }

        public async Task<List<WaterMeterValue>> GetRecordsAsync(long? waterMeterId, DateTime? fromDate)
        {
            var query = _context.Set<WaterMeterValue>().AsQueryable();
            
            if (fromDate.HasValue)
            {
                query = query.Where(e => e.ValueTime >= fromDate);

            }

            if (waterMeterId.HasValue)
            {
                query = query.Where(e => e.WaterMeterId == waterMeterId.Value);
            }

            var result = await query
                .OrderByDescending(e => e.ValueTime)
                .ToListAsync();

            return result;
        }
    }
}
