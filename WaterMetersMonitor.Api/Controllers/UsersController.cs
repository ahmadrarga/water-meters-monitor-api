using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterMetersMonitor.Api.Models.User;
using WaterMetersMonitor.Api.Models.WaterMeter;
using WaterMetersMonitor.Application.Repositories;
using WaterMetersMonitor.Application.Services;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Api.Controllers
{
    [Route("api/v1/Groups/{groupId}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _repo;
        private readonly IMapper _mapper;
        private readonly IRepository<WaterMeter> _meterRepository;

        public UsersController(IUserService repo, IRepository<WaterMeter> meterRepository, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _meterRepository = meterRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromRoute] long groupId, [FromBody] UserCreateDto payload)
        {
            var user = _mapper.Map<User>(payload);
            user.GroupId = groupId;
            var entity = await _repo.CreateUserAsync(user);

            return Created(string.Empty, entity);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] long groupId)
        {
            var users = await _repo.GetByGroupIdAsync(groupId);

            return Ok(users);
        }

        [HttpPost("{userId}/WaterMeters")]
        [ProducesResponseType(typeof(WaterMeter), StatusCodes.Status200OK)]

        public async Task<IActionResult> CreateWaterMeter(
            [FromRoute] long groupId,
            [FromRoute] long userId, 
            [FromBody] WaterMeterCreateDto payload)
        {
            var waterMeter = _mapper.Map<WaterMeter>(payload);
            waterMeter.UserId = userId;

            var entity = await _meterRepository.CreateAsync(waterMeter);

            return Created(string.Empty, entity);
        }

        [HttpGet("{userId}/WaterMeters")]
        [ProducesResponseType(typeof(List<WaterMeter>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWaterMeters([FromRoute] long groupId, [FromRoute] long userId)
        {
            var waterMeters = await _repo.GetUserWaterMetersAsync(userId);

            return Ok(waterMeters);
        }

        [HttpGet("{userId:long}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser([FromRoute] long groupId, [FromRoute] long userId)
        {
            var user = await _repo.GetUserAsync(userId);

            return Ok(user);
        }

        [HttpGet("{userUid}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByFirebaseUid([FromRoute] long groupId, [FromRoute] string userUid)
        {
            var user = await _repo.GetUserAsync(userUid);

            return Ok(user);
        }
    }
}
