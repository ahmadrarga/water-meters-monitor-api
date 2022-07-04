using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterMetersMonitor.Api.Extensions;
using WaterMetersMonitor.Api.Models.WaterMeter;
using WaterMetersMonitor.Application.Services;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Api.Controllers
{
    [Route("api/v1/Groups/{groupId}/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        public readonly ICalculationService _service;

        public RecordsController(ICalculationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromRoute] long groupId, [FromBody] Record payload)
        {
            var mainValue = payload.ToMainWaterMeterValue();
            var waterMetersValues = payload.ToWaterMetersValues();

            await _service.AddRecord(groupId, mainValue, waterMetersValues);

            return Created(string.Empty, new {});
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<WaterMeterValue>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecords([FromRoute] long groupId, [FromQuery] long? waterMeterId, [FromQuery] DateTime? fromDate)
        {
            var records = await _service.GetRecordsAsync(waterMeterId, fromDate);

            return Ok(records);
        }
    }
}
