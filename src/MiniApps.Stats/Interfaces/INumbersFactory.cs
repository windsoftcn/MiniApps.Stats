using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Interfaces
{
    public interface INumbersFactory
    {
        Task<long> CreateUserSequentailId(string appId);
    }
}
