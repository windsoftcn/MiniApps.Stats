using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get; set; }

        public List<MiniApp> MiniApps { get; set; }
    }

    public enum UserType
    {        
        /// <summary>
        /// 员工
        /// </summary>
        [DisplayName("员工")]
        Staff,

        /// <summary>
        /// 商户
        /// </summary>
        [DisplayName("商户")]
        Merchant
    }
}
