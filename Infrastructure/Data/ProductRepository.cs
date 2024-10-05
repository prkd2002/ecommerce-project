using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IReadOnlyList<ProductBrand>> GetALLProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
           return await _context.Products
                                .Include(product => product.ProductType)
                                .Include(product => product.ProductBrand)
                                .ToListAsync(); 
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

       public async Task<Product> GetProductByIdAsync(int id)
{
    return await _context.Products
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.Id == id);  // Utiliser FirstOrDefaultAsync pour les opérations asynchrones.
}

    }
}