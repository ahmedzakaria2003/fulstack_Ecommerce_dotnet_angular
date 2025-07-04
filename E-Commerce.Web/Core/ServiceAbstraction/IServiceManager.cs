using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
 public interface IServiceManager
    {

       public IProductService ProductService { get; }
       public IBaskettService CartService { get; }
       public IAuthenticationService AuthenticationService { get; }
       public IOrderService OrderService { get; }

        public IWishlistService WishlistService { get; }

        public IPayementService payementService { get; }    




    }
}
