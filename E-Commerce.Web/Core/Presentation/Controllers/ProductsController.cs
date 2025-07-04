using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get All Product
        [HttpGet]
        [Cache(60)]
        public async Task<ActionResult <PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams productQueryParams)

        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync( productQueryParams);
            return Ok(Products);

        }

        // Get Product By Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //Get All Types
        [HttpGet("types")]
        [Cache(60)]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypeAsync();
            return Ok(Types);
        }
        //Get All Brands
        [HttpGet("brands")]
        [Cache(60)]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandAsync();
            return Ok(Brands);
        }
    }
}
