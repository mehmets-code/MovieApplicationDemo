using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.Mvc.AutoMapper.Profiles;
using MovieApp.Mvc.Helpers;
using MovieApp.Mvc.Helpers.Abstract;
using MovieApp.Service.AutoMapper.Profiles;
using MovieApp.Service.Extensions;
using MovieApp.Services.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc
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
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boþ geçilmemelidir.");
            }).AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddAutoMapper(typeof(MovieProfile),typeof(UserProfile),typeof(ViewModelsProfile),typeof(CommentProfile), typeof(Movie2Profile));
            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IImageHelper,ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "MovieApp",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
