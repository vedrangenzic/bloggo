using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Bloggo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bloggo.Services;
using Bloggo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Hosting;
using System;

namespace Bloggo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Check Provider and get ConnectionString
            if (Configuration["Provider"] == "SQLite")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SQLite")));
            }
            else if (Configuration["Provider"] == "MySQL")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("MySQL")));
            }
            else if (Configuration["Provider"] == "MSSQL")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            else if (Configuration["Provider"] == "PostgreSQL")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            }
            // Exception
            else
            { throw new ArgumentException("Not a valid database type"); }

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
           

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opt =>
                {
                    //configure your other properties
                    opt.LoginPath = "/Identity/Account/Login";
                });



            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddControllersWithViews()
                 .AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddHealthChecks();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Index/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=BlogPost}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health" );
            });
        }
    }
}
