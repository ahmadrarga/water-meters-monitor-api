using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterMetersMonitor.Api.Models.Group;
using WaterMetersMonitor.Application.Repositories;
using WaterMetersMonitor.Application.Services;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IRepository<Group> _repo;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IRepository<Group> repo, IGroupService groupService, IMapper mapper)
        {
            _repo = repo;
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var group = await _repo.GetMany(e => e.Id == id)
                .Include(e => e.MainWaterMeter)
                .SingleAsync();

            return Ok(group);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Group), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] GroupCreateDto payload)
        {
            var group = _mapper.Map<Group>(payload);

            var entity = await _repo.CreateAsync(group);

            return Created("", entity);
        }
    }
}
