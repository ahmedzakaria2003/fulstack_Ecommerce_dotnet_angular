using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
 class ProductCountSpesicication : BaseSpecification<Product , int> 
    {
        public ProductCountSpesicication(ProductQueryParams productQueryParams) :base
           (P=>(!productQueryParams.BrandId.HasValue || P.ProductBrandId== productQueryParams.BrandId) &&
           (!productQueryParams.TypeId.HasValue||P.ProductTypeId== productQueryParams.TypeId)&&(
           string.IsNullOrWhiteSpace(productQueryParams.search)||(P.Name.ToLower()
           .Contains(productQueryParams.search.ToLower()))
           ))
        {
            // No includes needed for count specification
            // This specification is used to count products based on the query parameter
            // No ordering or paging is applied here, as it's only for counting






        }

    }
}
