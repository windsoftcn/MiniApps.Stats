﻿using MiniApps.Stats.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Interfaces
{
    public interface IMiniAppService
    {
        Task<bool> MiniAppExistsAsync(string appId);

        Task<List<MiniApp>> GetAllMiniApps();

        Task<MiniApp> GetMiniAppByAppId(string appId);
    }
}
