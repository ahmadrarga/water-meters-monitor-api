using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonIgnore]
        public DateTime? Updated { get; set; }   
    }

    public class Data
    {
        public int hStart { get; set; }
        public int hEnd { get; set; }
        public float? hPay { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string Name { get; set; }
        public int deserve { get; set; }
        public int mStart { get; set; }
        public int mEnd { get; set; }
        public float? mPay { get; set; }
    }
}
