using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.WshlistModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.WishlistDTO;

public class WishlistService : IWishlistService
{
    private readonly    IWishlistRepoistory _repository;
    private readonly IMapper _mapper;

    public WishlistService(IWishlistRepoistory repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WishlistDTO?> GetWishlistAsync(string key)
    {
        var wishlist = await _repository.GetWishlistAsync(key);
        return _mapper.Map<WishlistDTO>(wishlist);
    }

    public async Task<WishlistDTO?> CreateOrUpdateWishlistAsync(WishlistDTO wishlistDTO)
    {
        var wishlist = _mapper.Map<CustomerWishlist>(wishlistDTO);
        var updated = await _repository.CreateOrUpdateWishlistAsync(wishlist);
        return _mapper.Map<WishlistDTO>(updated);
    }

    public async Task<WishlistDTO?> RemoveItemFromWishlistAsync(string key, int productId)
    {
        var wishlist = await _repository.RemoveItemFromWishlistAsync(key, productId);
        return _mapper.Map<WishlistDTO>(wishlist);
    }
}
