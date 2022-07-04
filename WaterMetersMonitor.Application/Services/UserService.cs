using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Application.Errors;
using WaterMetersMonitor.Application.Exceptions;
using WaterMetersMonitor.Application.Repositories;
using WaterMetersMonitor.Domain.Entities;
using WaterMetersMonitor.Domain.Enums;
using WaterMetersMonitor.Infrastructure.DataContexts;

namespace WaterMetersMonitor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly SqlDataContext _context;

        public UserService(IRepository<User> repo, SqlDataContext context)
        {
            _repo = repo;
            _context = context;
        }

        public UserService()
        {
        }

        public async Task<User> GetAsync(long id)
        {
            var user = await _repo.GetMany()
                .Include(e => e.WaterMeters)
                .ThenInclude(e => e.WaterMeterValues)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (user == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }

            return user;
        }

        public async Task<User> CreateUserAsync(User entity)
        {
            entity.Role = UserRole.GroupCustomer;

            var user = await _repo.CreateAsync(entity);

            return user;
        }

        public async Task<List<User>> GetByGroupIdAsync(long groupId)
        {
            var users = await _context.Set<User>()
                .Include(e => e.WaterMeters)
                .Where(e => e.GroupId == groupId)
                .ToListAsync();

            return users;
        }

        public async Task<List<WaterMeter>> GetUserWaterMetersAsync(long userId)
        {
            var waterMeters = await _context.Set<WaterMeter>().Where(e => e.UserId == userId).ToListAsync();

            return waterMeters;
        }

        public async Task<User> GetUserAsync(long userId)
        {
            var user = await _context.Set<User>()
                .Include(e => e.WaterMeters)
                .FirstOrDefaultAsync(e => e.Id == userId);

            if (user == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }

            return user;
        }

        public async Task<User> GetUserAsync(string userUid)
        {
            var user = await _context.Set<User>()
                .Include(e => e.WaterMeters)
                .FirstOrDefaultAsync(e => e.FirebaseUid == userUid);

            if (user == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }

            return user;
        }
    }
}
