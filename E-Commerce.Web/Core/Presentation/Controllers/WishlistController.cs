using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using ServiceAbstraction;
using Shared.DataTransferObjects.WishlistDTO;


public class WishlistController(IServiceManager  _serviceManager) : ApiBaseController
{
    [HttpGet]
    [HttpGet("{id}")]

    public async Task<ActionResult<WishlistDTO>> GetWishlist(string id)
    {
        var wishlist = await _serviceManager.WishlistService.GetWishlistAsync(id);
        return wishlist is null ? NotFound() : Ok(wishlist);
    }

    [HttpPost]
    public async Task<ActionResult<WishlistDTO>> CreateOrUpdateWishlist(WishlistDTO wishlistDTO)
    {
        var updated = await _serviceManager.WishlistService.CreateOrUpdateWishlistAsync(wishlistDTO);
        return Ok(updated);
    }

    [HttpDelete("{key}/{productId}")]
    public async Task<ActionResult<WishlistDTO>> RemoveItem(string key, int productId)
    {
        var wishlist = await _serviceManager.WishlistService.RemoveItemFromWishlistAsync(key, productId);
        return wishlist is null
            ? NotFound(new { message = $"Item with id {productId} not found in wishlist '{key}'." })
            : Ok(wishlist);
    }
}
