using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Domain.Entities
{
    public class WaterMeterValue : BaseEntity
    {
        [Required]
        public DateTime ValueTime { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public long WaterMeterId { get; set; }

        [JsonIgnore]
        public virtual WaterMeter WaterMeter { get; set; }

        public float? Payment { get; set; }
    }
}
