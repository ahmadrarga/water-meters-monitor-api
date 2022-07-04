using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Application.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(long id);

        Task<User> CreateUserAsync(User entity);

        Task<List<User>> GetByGroupIdAsync(long groupId);

        Task<List<WaterMeter>> GetUserWaterMetersAsync(long userId);
        Task<User> GetUserAsync(long userId);
        Task<User> GetUserAsync(string userUid);
    }
}
