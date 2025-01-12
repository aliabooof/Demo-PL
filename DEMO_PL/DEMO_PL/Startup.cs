using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using DEMO_PL.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DEMO_PL
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
            services.AddControllersWithViews();

            services.AddDbContext<MVCDemoPLContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); //MultipleActiveResultSets = true;
            }/*,ServiceLifetime.Singleton*/);
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            //services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
      /*      services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();*/
            services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()) );
            services.AddScoped<IUnitPOfWork,UnitOfWork > ();
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<MVCDemoPLContext>()
                .AddDefaultTokenProviders();



           /* services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();                                   
            services.AddScoped<RoleManager <IdentityRole>>();*/
           // instead
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options=>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles(); //manification instead of multipule line all will be merged in one line
                                  //pundling like all css file will be 1 file so 1 request to all files 
                                  // photos for example

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
