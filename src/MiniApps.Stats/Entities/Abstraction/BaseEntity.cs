using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Entities
{
    public class BaseEntity : BaseEntity<int>
    {
    }


    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
