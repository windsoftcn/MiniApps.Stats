using MiniApps.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Interfaces
{
    public interface IAppStatsWriter
    {
        Task<bool> LoginAsync(AppUser appUser, DateTimeOffset? dateTime = null);

        Task<bool> ActiveAsync(AppUser appUser, DateTimeOffset? dateTime = null);

        Task<bool> NewUserAsync(AppUser appUser, DateTimeOffset? dateTime = null);
    }
}
