using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper 
        , IBasketRepoistory _cartRepoistory , UserManager<ApplicationUser> _userManager 
        , IConfiguration _configuration , IWishlistRepoistory _wishlist
        ) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork , _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBaskettService> _LazyCartService = new Lazy<IBaskettService>(() => new BasketService(_cartRepoistory , _mapper));
        public IBaskettService CartService => _LazyCartService.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager , _configuration , _mapper));

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _cartRepoistory, _unitOfWork));
        public IOrderService OrderService => _LazyOrderService.Value ;

        private readonly Lazy<IWishlistService> _LazyWishlistService = new Lazy<IWishlistService>(() => new WishlistService(_wishlist, _mapper));
        public IWishlistService WishlistService => _LazyWishlistService.Value;

        private readonly Lazy<IPayementService> _LazyPayementService = new Lazy<IPayementService>(() => new PayementService(_configuration,_cartRepoistory, _unitOfWork, _mapper));    
        public IPayementService payementService => _LazyPayementService.Value;
    }
}
