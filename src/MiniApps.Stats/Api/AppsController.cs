using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniApps.Stats.Extensions;
using MiniApps.Stats.Interfaces;
using MiniApps.Stats.Models;
using MiniApps.Stats.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Api
{
    [Route("api/[controller]")]
    public class AppsController : ControllerBase
    {
        private readonly IAppStatsWriter appStatsWriter;
        private readonly IMiniAppService miniAppService;
        private readonly IMapper mapper;

        public AppsController(IAppStatsWriter appStatsWriter,
            IMiniAppService miniAppService,
            IMapper mapper)
        {
            this.appStatsWriter = appStatsWriter ?? throw new ArgumentNullException(nameof(appStatsWriter));
            this.miniAppService = miniAppService ?? throw new ArgumentNullException(nameof(miniAppService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AppUserDto appUserDto)
        {
            AppUser appUser = mapper.Map<AppUserDto, AppUser>(appUserDto);

            if (string.IsNullOrWhiteSpace(appUser.Channel))
            {
                appUser.Channel = "default";
            }

            //if (!await miniAppService.MiniAppExistsAsync(appUser.AppId))
            //    return BadRequest("no appid exists.");

            var now = DateTimeOffset.Now.ToChinaStandardTime();

            // 新增用户
            await appStatsWriter.NewUserAsync(appUser, now);

            // 登录 
            await appStatsWriter.LoginAsync(appUser, now);

            // 活跃
            await appStatsWriter.ActiveAsync(appUser, now);

            return Ok("success");
        }
    }
}
