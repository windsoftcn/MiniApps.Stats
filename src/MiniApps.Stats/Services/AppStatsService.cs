using MiniApps.Stats.Extensions;
using MiniApps.Stats.Interfaces;
using MiniApps.Stats.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Services
{
    public class AppStatsService : IAppStatsReader, IAppStatsWriter
    {
        private const long OneDay = 86400;
        private const long OneHour = 3600;

        private readonly IAppUserService appUserService;
        private readonly IRedisFactory redisFactory;
        private readonly INumbersFactory numbersFactory;

        public AppStatsService(IAppUserService appUserService,
            IRedisFactory redisFactory,
            INumbersFactory numbersFactory)
        {
            this.appUserService = appUserService ?? throw new ArgumentNullException(nameof(appUserService));
            this.redisFactory = redisFactory ?? throw new ArgumentNullException(nameof(redisFactory));
            this.numbersFactory = numbersFactory ?? throw new ArgumentNullException(nameof(numbersFactory));
        }

        #region 统计写入

        public async Task<bool> ActiveAsync(AppUser appUser, DateTimeOffset? dateTime = null)
        {
            // 获取序号Id
            long sequentialId = await appUserService.GetUserSequentialIdAsync(appUser);
            if (sequentialId > 0)
            {
                // 获取记录时间
                DateTimeOffset now = dateTime ?? DateTimeOffset.Now.ToChinaStandardTime();

                // 判断用户当日是否活跃
                if (!await appUserService.IsAcitveAsync(appUser, sequentialId, now))
                {


                    // 标记当天 Active
                    await appUserService.MarkActiveAsync(appUser, sequentialId, now);

                    // 添加 Active 统计
                    await UpdateCounter(appUser.GetActiveCountKey(), OneHour, now);
                    await UpdateCounter(appUser.GetActiveCountKey(), OneDay, now);

                    return true;
                }
            }
            return false;
        }

        public async Task<bool> LoginAsync(AppUser appUser, DateTimeOffset? dateTime)
        {
            // 获取序号Id
            long sequentialId = await appUserService.GetUserSequentialIdAsync(appUser);

            if (sequentialId > 0)
            {
                // 获取记录时间
                DateTimeOffset now = dateTime ?? DateTimeOffset.Now.ToChinaStandardTime();

                // 标记当天Login
                await appUserService.MarkLoginAsync(appUser, sequentialId, now);

                // 添加登录统计
                await UpdateCounter(appUser.GetLoginCountKey(), OneHour, now);

                await UpdateCounter(appUser.GetLoginCountKey(), OneDay, now);

                return true;
            }

            return false;
        }

        public async Task<bool> NewUserAsync(AppUser appUser, DateTimeOffset? dateTime = null)
        {
            // 验证用户是否存在
            if (!await appUserService.ExistsAsync(appUser))
            {
                //获取序号Id
                long sequentialId = await numbersFactory.CreateUserSequentailId(appUser.AppId);

                if (sequentialId > 0 && await appUserService.AddUserAsync(appUser, sequentialId))
                {
                    // 获取记录时间
                    DateTimeOffset now = dateTime ?? DateTimeOffset.Now.ToChinaStandardTime();

                    // 按小时
                    await UpdateCounter(appUser.GetNewUserCountKey(), OneHour, now);

                    // 按天
                    await UpdateCounter(appUser.GetNewUserCountKey(), OneDay, now);

                    // 按天 + 渠道                     
                    await UpdateCounter(appUser.GetNewUserCountKeyByChannel(), OneDay, now);

                    return true;
                }
            }
            return false;
        }

        private Task<long> UpdateCounter(RedisKey key, long precision, DateTimeOffset? dateTime = null, long count = 1)
        {
            RedisKey combinedKey = $"{key}:{precision}";

            DateTimeOffset now = dateTime ?? DateTimeOffset.Now.ToChinaStandardTime();
            long startTime = (now.ToUnixTimeSeconds() / precision) * precision;

            return redisFactory.Database.HashIncrementAsync(combinedKey, startTime, count);
        }

        #endregion

        #region 统计读取



        #endregion

    }
}
