using MiniApps.Stats.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Extensions
{
    public static class AppUserExtensions
    {
        public static RedisKey GetLoginCountKey(this AppUser appUser)
        {
            RedisKey loginKey = $"{appUser.AppId}:LoginCount";
            return loginKey;
        }
                
        public static RedisKey GetActiveCountKey(this AppUser appUser)
        {
            RedisKey activeKey = $"{appUser.AppId}:ActiveCount";
            return activeKey;
        }

        public static RedisKey GetActiveMarkKey(this AppUser appUser)
        {
            RedisKey activeKey = $"{appUser.AppId}:ActiveMark";
            return activeKey;
        }

        public static RedisKey GetLoginMarkKey(this AppUser appUser)
        {
            RedisKey loginKey = $"{appUser.AppId}:LoginMark";
            return loginKey;
        }


        public static RedisKey GetNewUserCountKey(this AppUser appUser)
        {
            RedisKey newUserKey = $"{appUser.AppId}:NewUserCount";
            return newUserKey;
        }

        public static RedisKey GetNewUserCountKeyByChannel(this AppUser appUser)
        {
            RedisKey channelKey = $"{appUser.AppId}:NewUserCount:{appUser.Channel}";
            return channelKey;
        }
    }
}
