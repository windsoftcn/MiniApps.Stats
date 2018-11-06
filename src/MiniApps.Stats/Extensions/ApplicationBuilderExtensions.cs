using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MiniApps.Stats.Data;
using MiniApps.Stats.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDbContexts(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var statsDb =  scope.ServiceProvider.GetRequiredService<StatsDbContext>();
                statsDb.Database.Migrate();

                //var identityDb = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                //identityDb.Database.Migrate();
            }
        }
    }
}
