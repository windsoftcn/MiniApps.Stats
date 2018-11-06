using MiniApps.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Interfaces
{
    public interface IAppUserService
    {
        Task<bool> ExistsAsync(AppUser appUser);

        Task<long> GetUserSequentialIdAsync(AppUser appUser);

        Task<bool> AddUserAsync(AppUser appUser, long sequentialId);

        Task<bool> IsAcitveAsync(AppUser appUser, long offset,  DateTimeOffset dateTime);

        Task<bool> MarkLoginAsync(AppUser appUser, long offset, DateTimeOffset dateTime);

        Task<bool> MarkActiveAsync(AppUser appUser, long offset, DateTimeOffset dateTime);
    }
}
