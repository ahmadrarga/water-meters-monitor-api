using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Application.Services
{
    public interface ICalculationService
    {
        Task AddRecord(long groupId, MainWaterMeterValue mainValue, List<WaterMeterValue> waterMeterValues);

        Task<List<WaterMeterValue>> GetRecordsAsync(long? waterMeterId, DateTime? fromDate);
    }
}
