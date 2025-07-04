using DomainLayer.Contracts;
using DomainLayer.Models.WshlistModule;
using StackExchange.Redis;
using System.Text.Json;

public class WishlistRepoistory : IWishlistRepoistory
{
    private readonly IDatabase _database;
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };

    public WishlistRepoistory(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerWishlist?> GetWishlistAsync(string key)
    {
        var data = await _database.StringGetAsync(key);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerWishlist>(data!, _serializerOptions);
    }

    public async Task<CustomerWishlist?> CreateOrUpdateWishlistAsync(CustomerWishlist wishlist, TimeSpan? ttl = null)
    {
        var data = JsonSerializer.Serialize(wishlist);
        await _database.StringSetAsync(wishlist.Id, data, ttl);
        return await GetWishlistAsync(wishlist.Id);
    }

    public async Task<CustomerWishlist?> RemoveItemFromWishlistAsync(string key, int productId)
    {
        var wishlist = await GetWishlistAsync(key);
        if (wishlist == null) return null;

        var item = wishlist.Items.FirstOrDefault(i => i.Id == productId);
        if (item is null) return null;

        wishlist.Items.Remove(item);
        await CreateOrUpdateWishlistAsync(wishlist);
        return wishlist;
    }
}
