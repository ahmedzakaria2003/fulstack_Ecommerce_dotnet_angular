using DomainLayer.Models.CartModule;
using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepoistory
    {

        Task<CustomerBasket?> GetCartAsync(string Key);

        Task<CustomerBasket?> CreateOrUpdateCartAsync(CustomerBasket cart , TimeSpan? TimeToLive = null );

        Task<bool> DeleteCartAsync(string id);


    }
}
