using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.CartModule;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.DataTransferObjects.CartDTOS;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepoistory(IConnectionMultiplexer connection) : IBasketRepoistory
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateCartAsync(CustomerBasket cart, TimeSpan? TimeToLive = null)
        {
            var serializedCart = JsonSerializer.Serialize(cart);
            var IsCreatedOrUpdated = await _database.StringSetAsync(cart.Id, serializedCart, TimeToLive ?? TimeSpan.FromDays(30));

            if (IsCreatedOrUpdated)
            {
                return await GetCartAsync(cart.Id);
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetCartAsync(string Key)
        {
            var Cart = await _database.StringGetAsync(Key);
            if (Cart.IsNullOrEmpty)
                return null;

            else
                return JsonSerializer.Deserialize<CustomerBasket>(Cart!);



        }

  

    }
}
