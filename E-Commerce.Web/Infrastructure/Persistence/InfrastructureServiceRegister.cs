using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
   public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddScoped<IDataSeeding, DataSeed>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepoistory, BasketRepoistory>();
            Services.AddScoped<ICacheRepository , CacheRepository>();
            Services.AddScoped<IWishlistRepoistory, WishlistRepoistory>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) => {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
                });
         

            Services.AddDbContext<StoreDbContext>
     (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


    Services.AddDbContext<StoreIdentityDbContext>
(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
                

            return Services;

        }
    }
}
