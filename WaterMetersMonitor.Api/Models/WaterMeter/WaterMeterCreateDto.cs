using System.ComponentModel.DataAnnotations;

namespace WaterMetersMonitor.Api.Models.WaterMeter
{
    public class WaterMeterCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public float CubicMetersAllowed { get; set; }
    }
}
