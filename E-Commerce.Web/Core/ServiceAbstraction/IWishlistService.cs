using Shared.DataTransferObjects.WishlistDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IWishlistService
    {
        Task<WishlistDTO?> GetWishlistAsync(string key);
        Task<WishlistDTO?> CreateOrUpdateWishlistAsync(WishlistDTO wishlistDTO);
        Task<WishlistDTO?> RemoveItemFromWishlistAsync(string key, int productId);
    }
}
