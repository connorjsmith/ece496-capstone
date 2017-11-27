using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.Identity;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace PillDispenserWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureCommonServices(IServiceCollection services)
        {
            // Tell the program that we want to use the newly registered ApplicationDbContext for our ApplicaitonUser store
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 3;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(7);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Denied";
                options.SlidingExpiration = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            // Set up the database from our settings json
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionIdentityConnection"));
            });

            services.AddDbContext<AppDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionDataConnection"));
            });
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            // Set up the database we want to use
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DebugIdentityConnection"));
            });

            services.AddDbContext<AppDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DebugDataConnection"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            // Automatically apply any pending migrations
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<IdentityContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<AppDataContext>().Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                // TODO log something here
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Error/{0}"); // this will treat any nonsense URLs as if the user navigated to /Error/{error code}.
                                                               // for instance: /Error/404 is defined in NotFoundController.cs
            app.UseMvc(); // we define routes in the controllers themselves
        }
    }
}
