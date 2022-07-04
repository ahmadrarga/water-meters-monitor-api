using System.ComponentModel.DataAnnotations;

namespace WaterMetersMonitor.Api.Models.WaterMeter
{
    public class MainWaterMeterCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
