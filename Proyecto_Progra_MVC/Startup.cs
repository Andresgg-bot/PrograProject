using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proyecto_Progra_MVC.Application;
using Proyecto_Progra_MVC.Application.Contracts.Data;
using Proyecto_Progra_MVC.Components;
using Proyecto_Progra_MVC.Contracts;
using Proyecto_Progra_MVC.Domain.Models.ConfigurationModels;
using Proyecto_Progra_MVC.Infraestructure;
using Proyecto_Progra_MVC.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC
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
            services.RegisterApplicationServices(Configuration);
            services.RegisterInfraestructureServices(Configuration);

            services.AddAuthentication
                (
                    options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    }
                )
                .AddCookie
                    (
                        options =>
                        {
                            options.LoginPath = "/accounts/login";
                            options.LogoutPath = "/accounts/logout";
                            options.AccessDeniedPath = "/accounts/accessdenied";
                            options.Cookie.SameSite = SameSiteMode.Lax;
                        }
                     )
              .AddGoogle
                    (
                        options =>
                        {
                            options.ClientId =
                                Configuration.GetValue<string>("GoogleAuthenticationConfiguration:ClientId");
                            options.ClientSecret =
                                Configuration.GetValue<string>("GoogleAuthenticationConfiguration:ClientSecret");
                        }
                    );

            services.Configure<IdentityOptions>
                (
                    options =>
                    {
                        options.Password.RequiredLength = 8;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequiredUniqueChars = 1;
                    }
                );

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            /*services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());*/

            services.Configure<RecaptchaConfiguration>(Configuration.GetSection("RecaptchaConfiguration"));

            services.AddSingleton<IRecaptchaValidator, RecaptchaValidator>();
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
