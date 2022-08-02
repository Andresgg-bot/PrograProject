using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_MVC.Application.Configurations;
using Proyecto_Progra_MVC.Application.Contracts.Managers;
using Proyecto_Progra_MVC.Application.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Application
{
    public static class Injection
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfiguration
                jwtConfiguration =
                    configuration.GetSection("JwtConfiguration")
                        .Get<JwtConfiguration>();

            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            services.AddSingleton<IJwtManager, JwtManager>();

            services.AddAuthentication
                (
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer
                    (
                        options =>
                        {
                            var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SigninKey);

                            options.SaveToken = true;
                            options.TokenValidationParameters =
                                new TokenValidationParameters
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = jwtConfiguration.Issuer,
                                    ValidAudience = jwtConfiguration.Audience,
                                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                                };
                        }
                    );

            return services;
        }
    }
}
