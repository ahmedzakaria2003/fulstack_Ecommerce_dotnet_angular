using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Service.Specification;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandAsync()
        {
          var Repo = _unitOfWork.GetRepository<ProductBrand , int>();
             var Brands = await Repo.GetAllAsync();
            var BrandsDTO = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDTO>>(Brands);
            return BrandsDTO;
        }


        public async Task<IEnumerable<TypeDTO>> GetAllTypeAsync()
        {
            var Repo =  _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var TypesDTO = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
            return TypesDTO;

        }


        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams productQueryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();

            // Get paged products with filters and sorting
            var specification = new ProductWithBrandAndTypeSpecifications(productQueryParams);
            var pagedProducts = await repo.GetAllAsync(specification);

            // Get full count (without paging) for pagination UI
            var totalCount = await repo.CountAsync(new ProductCountSpesicication(productQueryParams));

            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(pagedProducts);

            return new PaginatedResult<ProductDTO>(
                productQueryParams.pageNumber,
                productQueryParams.PageSize, // ✅ ثابت، مش عدد النتائج الراجعة
                totalCount,
                data
            );


        }
        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var Sepcifications = new ProductWithBrandAndTypeSpecifications(id);
   
            var ProductRepo = await _unitOfWork.GetRepository<Product, int>().GetbyIdAsync(Sepcifications);
            if (ProductRepo is null)
            {
                throw new ProductNotFoundExeptions(id);
            }
            var ProductDTO =  _mapper.Map<Product, ProductDTO>(ProductRepo);

            return ProductDTO;

        }

     
    }
}
