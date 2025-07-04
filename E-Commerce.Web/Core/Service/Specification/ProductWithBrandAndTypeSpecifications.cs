using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
  class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product , int>
    {


        public ProductWithBrandAndTypeSpecifications(ProductQueryParams productQueryParams) :base
           (P=>(!productQueryParams.BrandId.HasValue || P.ProductBrandId== productQueryParams.BrandId) &&
           (!productQueryParams.TypeId.HasValue||P.ProductTypeId== productQueryParams.TypeId)&&(
           string.IsNullOrWhiteSpace(productQueryParams.search)||(P.Name.ToLower()
           .Contains(productQueryParams.search.ToLower()))
           ))
        {

            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            if (productQueryParams.sort == ProductSorting.PriceAsc)
            {
                AddOrderBy(P => P.Price);
            }
            else if (productQueryParams.sort == ProductSorting.PriceDesc)
            {
                AddOrderByDescending(P => P.Price);
            }
            else if (productQueryParams.sort == ProductSorting.NameAsc)
            {
                AddOrderBy(P => P.Name);
            }
            else if (productQueryParams.sort == ProductSorting.NameDesc)
            {
                AddOrderByDescending(P => P.Name);
            }

            ApplyPaging(productQueryParams.PageSize , productQueryParams.pageNumber);

        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }



    }
}
