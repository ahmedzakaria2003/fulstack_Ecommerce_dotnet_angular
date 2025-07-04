using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
   public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams productQueryParams);
        Task<ProductDTO?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypeAsync();





    }
}
