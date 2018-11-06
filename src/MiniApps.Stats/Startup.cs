using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniApps.Stats.Data;
using MiniApps.Stats.Factories;
using MiniApps.Stats.Identity;
using MiniApps.Stats.Interfaces;
using MiniApps.Stats.Services;
using StackExchange.Redis;

namespace MiniApps.Stats
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<StatsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppStatsDb")));

            //services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredUniqueChars = 1;
            //    options.User.RequireUniqueEmail = false;
            //    options.SignIn.RequireConfirmedEmail = false;
            //    options.SignIn.RequireConfirmedPhoneNumber = false;

            //}).AddEntityFrameworkStores<AppIdentityDbContext>()
            //.AddDefaultTokenProviders();

            services.AddMemoryCache();
            services.AddAutoMapper();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisDb")));
            services.AddTransient<IRedisFactory, RedisFactory>();
            services.AddSingleton<INumberGenerator, NumberGenerator>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IAppStatsReader, AppStatsService>();
            services.AddTransient<IAppStatsWriter, AppStatsService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Database-Update
            app.MigrateDbContexts();
        }
    }
}
