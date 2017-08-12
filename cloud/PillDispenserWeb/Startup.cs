using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.Identity;
using PillDispenserWeb.Models.Implementations;
using PillDispenserWeb.Models.Interfaces;

namespace PillDispenserWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // Set up the database we want to use
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("Server={localdb}\\mssqllocaldb;Database=YoutubeWeb")
            );

            // Tell the program that we want to use the newly registered ApplicationDbContext for our ApplicaitonUser store
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Production Data Accessors
            services.AddSingleton<IDoctorRepository, DoctorRepositoryImpl>();
            services.AddSingleton<IMedicationRepository, MedicationRepositoryImpl>();
            services.AddSingleton<IPatientRepository, PatientRepositoryImpl>();

            services.AddMvc();
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // Set up the database we want to use
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("Server={localdb}\\mssqllocaldb;Database=YoutubeWeb")
            );

            // Tell the program that we want to use the newly registered ApplicationDbContext for our ApplicaitonUser store
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Development Data Accessors
            services.AddSingleton<IDoctorRepository, DoctorRepositoryImpl>();
            services.AddSingleton<IMedicationRepository, MedicationRepositoryImpl>();
            services.AddSingleton<IPatientRepository, PatientRepositoryImpl>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseStatusCodePagesWithReExecute("/Error/{0}"); // this will treat any nonsense URLs as if the user navigated to /Error/{error code}.
                                                               // for instance: /Error/404 is defined in NotFoundController.cs
            app.UseMvc(); // we define routes in the controllers themselves
        }
    }
}
