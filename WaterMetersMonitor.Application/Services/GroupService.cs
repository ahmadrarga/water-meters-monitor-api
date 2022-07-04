using Microsoft.EntityFrameworkCore;
using System;
using WaterMetersMonitor.Application.Errors;
using WaterMetersMonitor.Application.Exceptions;
using WaterMetersMonitor.Application.Repositories;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _repo;

        public GroupService(IRepository<Group> repo)
        {
            _repo = repo;
        }

        public async Task<Group> GetGroupAsync(long groupId)
        {
            var group = await _repo.GetMany(e => e.Id == groupId)
                .Include(e => e.Users)
                .ThenInclude(e => e.WaterMeters)
                .Include(e => e.MainWaterMeter)
                .SingleOrDefaultAsync();

            if (group == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }

            return group;
        }
    }
}
