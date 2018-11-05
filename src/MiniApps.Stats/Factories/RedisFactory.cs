using MiniApps.Stats.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Factories
{
    public class RedisFactory : IRedisFactory
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisFactory(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        }

        private IDatabase _database;

        public IDatabase Database => _database ?? (_database = connectionMultiplexer.GetDatabase());

        public Task<long> StringIncrementAsync(RedisKey key, long value = 1) => Database.StringIncrementAsync(key, value);

        public Task<double> StringIncrementAsync(RedisKey key, double value = 1) => Database.StringIncrementAsync(key, value);

        public Task<bool> HashSetAsync(RedisKey key, RedisValue field, RedisValue value) => Database.HashSetAsync(key, field, value);
            
        
     }
}
