using MiniApps.Stats.Extensions;
using MiniApps.Stats.Interfaces;
using MiniApps.Stats.Models;
using MiniApps.Stats.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Services
{
    public class AppUserService : IAppUserService
    { 
        private readonly IRedisFactory redisFactory;

        public AppUserService(IRedisFactory redisFactory)
        {
            this.redisFactory = redisFactory ?? throw new ArgumentNullException(nameof(redisFactory));
        }

        public Task<bool> AddUserAsync(AppUser appUser, long sequentialId)
        {
            var userKey = GetUserKey(appUser);          

            return redisFactory.HashSetAsync(userKey , appUser.UserId,sequentialId);
        }

        public Task<bool> ExistsAsync(AppUser appUser)
        {
            var userKey = GetUserKey(appUser);
            return redisFactory.Database.HashExistsAsync(userKey, appUser.UserId);
        }

        public async Task<long> GetUserSequentialIdAsync(AppUser appUser)
        {
            var userKey = GetUserKey(appUser);

            RedisValue value = await redisFactory.Database.HashGetAsync(userKey, appUser.UserId);
            if (value.IsInteger)
            {
                return (long)value;
            }
            return default(long);
        }

        private RedisKey GetUserKey(AppUser appUser)
        {
            string suffix = HashSplit(appUser.UserId, 2);
            RedisKey userKey = $"{appUser.AppId}:Users:{suffix}";
            return userKey;
        }

        private string HashSplit(string value, int offset = 2)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            var hashedId = HashHelper.GetMD5(value);
            return hashedId?.Substring(0, offset).ToUpper();
        }

        public Task<bool> IsAcitveAsync(AppUser appUser, long offset, DateTimeOffset dateTime)
        {

            RedisKey activeKey = $"{appUser.GetActiveMarkKey()}:{dateTime.ToString("yyyyMMdd")}";

            return redisFactory.Database.StringGetBitAsync(activeKey, offset);
        }

        public Task<bool> MarkLoginAsync(AppUser appUser, long offset, DateTimeOffset dateTime)
        {
            RedisKey loginKey = $"{appUser.GetLoginMarkKey()}:{dateTime.ToString("yyyyMMdd")}";

            return redisFactory.Database.StringGetBitAsync(loginKey, offset);
        }

        public Task<bool> MarkActiveAsync(AppUser appUser, long offset, DateTimeOffset dateTime)
        {
            RedisKey activeKey = $"{appUser.GetActiveMarkKey()}:{dateTime.ToString("yyyyMMdd")}";

            return redisFactory.Database.StringSetBitAsync(activeKey, offset, true);
        }
    }
}
