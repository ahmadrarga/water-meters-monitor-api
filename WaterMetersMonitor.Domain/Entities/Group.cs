using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Domain.Entities
{
    public class Group : BaseEntity
    {
        [MaxLength(100)]
        public string GroupName { get; set; }

        [JsonIgnore]
        public bool IsDisabled { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual MainWaterMeter MainWaterMeter { get; set; }
    }
}
