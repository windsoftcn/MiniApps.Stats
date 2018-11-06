using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Interfaces
{
    public interface IRedisFactory
    {
        IDatabase Database { get; }

        Task<bool> HashSetAsync(RedisKey key, RedisValue field, RedisValue value);
    }
}
