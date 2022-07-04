using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WaterMetersMonitor.Api.Models.WaterMeter;

namespace WaterMetersMonitor.Api.Models.Group
{
    public class GroupCreateDto
    {
        [MaxLength(100)]
        public string GroupName { get; set; }

        public MainWaterMeterCreateDto MainWaterMeter { get; set; }
    }
}
