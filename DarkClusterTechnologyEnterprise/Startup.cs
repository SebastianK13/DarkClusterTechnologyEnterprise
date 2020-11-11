using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DarkClusterTechnologyEnterprise.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace DarkClusterTechnologyEnterprise
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
       
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DCTEIdentity:ConnectionString"]);
                options.UseLazyLoadingProxies(true);
            });


            services.AddDbContext<AppServiceDeskDbContext>(options =>
                           options.UseSqlServer(
                               Configuration["Data:DCTEServiceDesk:ConnectionString"]));

            services.AddDbContext<AppCustomersDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DCTECustomers:ConnectionString"]);
                options.UseLazyLoadingProxies(true);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                //opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
                            .AddDefaultTokenProviders();

            services.AddTransient<IEmployeeRepository, EFEmployeeRepository>();
            services.AddTransient<IServiceDeskRepository, EFServiceDeskRepository>();
            services.AddTransient<ICustomerRepository, EFCustomerRepository>();

            //services.AddControllers()
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});
            services.AddControllers()
              .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();

        }
    }
}
