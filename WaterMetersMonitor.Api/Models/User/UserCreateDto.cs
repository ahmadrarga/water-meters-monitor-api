using System.ComponentModel.DataAnnotations;
using WaterMetersMonitor.Api.Models.WaterMeter;

namespace WaterMetersMonitor.Api.Models.User
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirebaseUid { get; set; }

        public string? PhoneNumber { get; set; }

        public List<WaterMeterCreateDto> WaterMeters { get; set; }
    }
}
