using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Enums;

namespace WaterMetersMonitor.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirebaseUid { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public long GroupId { get; set; }

        public UserRole Role { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        public virtual ICollection<WaterMeter> WaterMeters { get; set; }
    }
}
