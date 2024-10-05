using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsControllers:ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsControllers(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("allProducts")]
        public async Task<IActionResult> GetAllProducts(){
            var Products = await _repository.GetAllProductsAsync();

            return Ok(Products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById([FromRoute]int id)
        {
            var Product = await  _repository.GetProductByIdAsync(id);
            return Ok(Product);
        }


        [HttpGet("productType")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductTypes()
        {
            var ProductTypes = _repository.GetAllProductTypesAsync();
            return Ok(
                ProductTypes);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrands()
        {
            var ProductBrands = _repository.GetALLProductBrandsAsync();
            return Ok(
                ProductBrands
            );
        }

        
    }
}