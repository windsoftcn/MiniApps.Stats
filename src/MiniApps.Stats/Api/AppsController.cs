using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
    public class AppsController : ControllerBase
    {
        private readonly IAppStatsWriter appStatsWriter;
        private readonly IMapper mapper;

        public AppsController(IAppStatsWriter appStatsWriter,
            IMapper mapper)
        {
            this.appStatsWriter = appStatsWriter ?? throw new ArgumentNullException(nameof(appStatsWriter));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AppUserDto appUserDto)
        {
            AppUser appUser =  mapper.Map<AppUserDto, AppUser>(appUserDto);

            var now = DateTimeOffset.Now;
            // 新增用户
            await appStatsWriter.NewUserAsync(appUser, now);
            
            // 登录 
            await appStatsWriter.LoginAsync(appUser, now);

            // 活跃
            await appStatsWriter.ActiveAsync(appUser, now);

            return Ok();
        }


        [HttpPost("active")]
        public async Task<IActionResult> ActiveAsync([FromBody] AppUserDto appUserDto)
        {
            AppUser appUser = mapper.Map<AppUserDto, AppUser>(appUserDto);

            var now = DateTimeOffset.Now;

            // 活跃
            await appStatsWriter.ActiveAsync(appUser, now);

            return Ok();
        }


        
    }
}
