using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Application.Services
{
    public interface IGroupService
    {
        Task<Group> GetGroupAsync(long groupId);
    }
}
