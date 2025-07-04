﻿using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public static class ApplicationServiceRegister
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly); 
           Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }

    }
}
