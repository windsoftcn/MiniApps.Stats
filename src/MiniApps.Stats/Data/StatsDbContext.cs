using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniApps.Stats.Entities;
using MiniApps.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Data
{
    public class StatsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MiniApp> MiniApps { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }


        public StatsDbContext(DbContextOptions<StatsDbContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
