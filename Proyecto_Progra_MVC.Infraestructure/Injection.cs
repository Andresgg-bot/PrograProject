﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proyecto_Progra_MVC.Application.Contracts.Managers;
using Proyecto_Progra_MVC.Application.Managers;
using Proyecto_Progra_MVC.Domain.Models.ConfigurationModels;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>
                (TokenOptions.DefaultProvider);

            return services;
        }
    }
}