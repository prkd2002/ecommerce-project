using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsControllers:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductsControllers(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("allProducts")]
        public async Task<IActionResult> GetAllProducts(){
            var Products = await _applicationDbContext.Products.ToListAsync();

            return Ok(Products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById([FromRoute]int id)
        {
            var Product = await  _applicationDbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
            return Ok(Product);
        }

        
    }
}