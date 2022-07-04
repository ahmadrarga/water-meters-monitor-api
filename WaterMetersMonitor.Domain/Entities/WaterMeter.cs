using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Domain.Entities
{
    public class WaterMeter : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [Required]
        public float CubicMetersAllowed { get; set; }

        public virtual ICollection<WaterMeterValue> WaterMeterValues { get; set; }
    }
}
