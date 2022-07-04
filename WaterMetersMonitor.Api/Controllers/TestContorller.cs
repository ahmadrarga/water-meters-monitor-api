using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WaterMetersMonitor.Domain.Entities;
using WaterMetersMonitor.Infrastructure.DataContexts;

namespace WaterMetersMonitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestContorller : ControllerBase
    {
        private readonly SqlDataContext _dataContext;

        public TestContorller(SqlDataContext context)
        {
            _dataContext = context;
        }

        [HttpPost("test")]
        public string res([FromBody] List<Data> data)
        {
            Dictionary<string, long> d = new Dictionary<string, long>();
            d["מחייסן"] = 5;
            d["מחייסן סדה"] = 4;
            d["תייסיר"] = 9;
            d["סלימאן"] = 7;
            d["סאלח"] = 11;
            d["נאיף"] = 6;
            d["אבראהים"] = 8;
            d["יאסר"] = 10;

            foreach (var row in data)
            {
                var main = new MainWaterMeterValue()
                {
                    MainWaterMeterId = 1,
                    Value = row.mStart,
                    ValueTime = new DateTime(row.year, row.month, 1),
                    Payment = row.mPay
                };

                var h = new WaterMeterValue()
                {
                    WaterMeterId = d[row.Name],
                    Value = row.hStart,
                    ValueTime = new DateTime(row.year, row.month, 1),
                    Payment=row.hPay
                };

                _dataContext.Entry(main).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _dataContext.Entry(h).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            _dataContext.SaveChanges();
            return "";
        }
    }
}
