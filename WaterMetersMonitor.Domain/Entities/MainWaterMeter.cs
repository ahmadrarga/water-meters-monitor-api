using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Domain.Entities
{
    public class MainWaterMeter : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public long GroupId { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        public virtual ICollection<MainWaterMeterValue> MainWaterMeterValues { get; set; }
    }
}
