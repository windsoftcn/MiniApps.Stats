using MiniApps.Stats.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Factories
{
    public class NumbersFactory : INumbersFactory
    {        
        private readonly IRedisFactory redisFactory;

        public NumbersFactory(IRedisFactory redisFactory)
        { 
            this.redisFactory = redisFactory ?? throw new ArgumentNullException(nameof(redisFactory));
        }         

        private readonly object _userSequentialId = new object();       

        public Task<long> CreateUserSequentailId(string appId)
        {
            RedisKey key = $"{appId}:SequentialIds";
            RedisValue field = "UserId";
            lock (_userSequentialId)
            {
                return redisFactory.Database.HashIncrementAsync(key, field, 1);
            }
        }
    }
}
