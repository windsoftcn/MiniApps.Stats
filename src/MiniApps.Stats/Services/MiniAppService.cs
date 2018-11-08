using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MiniApps.Stats.Data;
using MiniApps.Stats.Entities;
using MiniApps.Stats.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Services
{
    public class MiniAppService : IMiniAppService
    {
        private const string Keys_MinApps = "AllMiniApps";

        private readonly StatsDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public MiniAppService(StatsDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        private IList<MiniApp> _miniApps;

        public IList<MiniApp> MiniApps => _miniApps ?? (_miniApps = GetMiniAppsCache());

        public async Task<bool> MiniAppExistsAsync(string appId)
        {
            if (MiniApps != null && MiniApps.Any(ma => ma.AppId == appId))
            {
                return true;
            }
            var apps = await GetAllMiniApps();

            if (apps != null && apps.Any(ma => ma.AppId == appId))
            {
                SetMiniAppsCache(apps);
                return true;
            }
            return false;
        }


        private IList<MiniApp> GetMiniAppsCache()
        {
            return memoryCache.Get<IList<MiniApp>>(Keys_MinApps);
        }

        private IList<MiniApp> SetMiniAppsCache(IList<MiniApp> apps)
        {
            if (apps != null && apps.Count == 0)
            {
                return memoryCache.Set<IList<MiniApp>>(Keys_MinApps, apps);
            }
            return null;
        }

      

        public Task<List<MiniApp>> GetAllMiniApps()
        {
            return dbContext.MiniApps
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<MiniApp> GetMiniAppByAppId(string appId)
        {
            return dbContext.MiniApps
                .Where(ma => ma.AppId == appId)
                .FirstOrDefaultAsync();
        }

       
    }
}
