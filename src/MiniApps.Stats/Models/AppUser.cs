using MiniApps.Stats.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Models
{
    public class AppUser
    {
        public string AppId { get; set; }

        public string UserId { get; set; }

        public string Channel { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now.ToChinaStandardTime();
    }
}
