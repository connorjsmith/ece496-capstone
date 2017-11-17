﻿using Microsoft.AspNetCore.Builder;
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
using GenFu;
using PillDispenserWeb.Models.DataTypes;
using System;
using System.Linq;
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
            services.AddDbContext<IdentityContext>( options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionIdentityConnection"));
            });

            // Production Data Accessors
            // TODO: this will break production builds until we add singletons for IQueryable<Doctor> etc. to point to our real databases
            services.AddSingleton<IDoctorRepository>(new DoctorRepositoryImpl(null));
            services.AddSingleton<IMedicationRepository>(new MedicationRepositoryImpl(null));
            services.AddSingleton<IPatientRepository>(new PatientRepositoryImpl(null));
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            // Set up the database we want to use
            services.AddDbContext<IdentityContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DebugIdentityConnection"));
            });
            
            // Development mock data
            var rng = new Random();
            // 100 mock doctors
            A.Configure<Doctor>()
                .Fill(d => d.DoctorId, () => Guid.NewGuid().ToString())
                .Fill(d => d.PhoneNumber).AsPhoneNumber()
                .Fill(d => d.FirstName).AsFirstName()
                .Fill(d => d.LastName).AsLastName()
                .Fill(d => d.EmailAddress, d => $"{d.FirstName}.{d.LastName}@email.com");
            IQueryable<Doctor> mockDoctorsRepo = A.ListOf<Doctor>(100).AsQueryable();

            // 50 mock medications
            A.Configure<Medication>()
                .Fill(m => m.DosageInMg, () => (float)Math.Round(rng.NextDouble() * 100, 2))
                .Fill(m => m.MedicationId, () => Guid.NewGuid().ToString())
                .Fill(m => m.PlaintextName).AsLoremIpsumWords(3);
            IQueryable<Medication> mockMedicationRepo = A.ListOf<Medication>(50).AsQueryable();

            // 100 mock patients with doctors
            A.Configure<Patient>()
                .Fill(p => p.FirstName).AsFirstName()
                .Fill(p => p.LastName).AsLastName()
                .Fill(p => p.DoctorIds, () =>
                {
                    // Assign the patient between 1 and 5 random doctors
                    int numDoctors = rng.Next(1, 5);
                    string[] doctorIds = mockDoctorsRepo.OrderBy(x => rng.NextDouble()).Take(numDoctors).Select(d => d.DoctorId).ToArray();
                    return doctorIds;
                })
                .Fill(p => p.PhoneNumber).AsPhoneNumber()
                .Fill(p => p.EmailAddress, p => $"{p.FirstName}.{p.LastName}@email.com");
            IQueryable<Patient> mockPatientRepo = A.ListOf<Patient>(100).AsQueryable();

            // Development Data Accessors
            services.AddSingleton<IDoctorRepository>(new DoctorRepositoryImpl(mockDoctorsRepo));
            services.AddSingleton<IMedicationRepository>(new MedicationRepositoryImpl(mockMedicationRepo));
            services.AddSingleton<IPatientRepository>(new PatientRepositoryImpl(mockPatientRepo));
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
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Error/{0}"); // this will treat any nonsense URLs as if the user navigated to /Error/{error code}.
                                                               // for instance: /Error/404 is defined in NotFoundController.cs
            app.UseMvc(); // we define routes in the controllers themselves
        }
    }
}
