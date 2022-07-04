

namespace WaterMetersMonitor.Api.Models.WaterMeter
{
    public class Record
    {
        public long MainWaterMeterValue { get; set; }

        public List<WaterMeterRecord> WaterMeters { get; set; }

        public float Payment { get; set; }
    }

    public class WaterMeterRecord
    {
        public long WaterMeterId { get; set; }

        public float Value { get; set; }
    }
}
