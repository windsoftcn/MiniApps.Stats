using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Entities
{
    public class MiniApp : BaseEntity
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string AppName { get; set; }
    }    
}
