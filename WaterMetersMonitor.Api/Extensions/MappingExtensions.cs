using WaterMetersMonitor.Api.Models.WaterMeter;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Api.Extensions
{
    public static class MappingExtensions
    {
        public static MainWaterMeterValue ToMainWaterMeterValue(this Record record)
        {
            return new MainWaterMeterValue
            {
                Value = record.MainWaterMeterValue
            };
        }

        public static List<WaterMeterValue> ToWaterMetersValues(this Record record)
        {
            return record.WaterMeters.Select(value => value.ToWaterMeterValue()).ToList();
        }

        public static WaterMeterValue ToWaterMeterValue(this WaterMeterRecord record)
        {
            return new WaterMeterValue
            {
                Value = record.Value,
                WaterMeterId = record.WaterMeterId,
            };
        }
    }
}
