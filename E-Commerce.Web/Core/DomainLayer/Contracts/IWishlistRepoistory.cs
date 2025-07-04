using DomainLayer.Models.WshlistModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IWishlistRepoistory
    {
        Task<CustomerWishlist?> GetWishlistAsync(string key);
        Task<CustomerWishlist?> CreateOrUpdateWishlistAsync(CustomerWishlist wishlist, TimeSpan? ttl = null);
        Task<CustomerWishlist?> RemoveItemFromWishlistAsync(string key, int productId);
    }
}
