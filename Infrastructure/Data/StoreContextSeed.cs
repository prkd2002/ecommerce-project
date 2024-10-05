using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        
        public static async Task SeedAsync(ApplicationDbContext context , ILoggerFactory loggerFactory)
        {
            try{
                var options = new JsonSerializerOptions
                {
                        PropertyNameCaseInsensitive = true
                };
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData,options);
                    foreach(var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }



                if(!context.ProductTypes.Any())
                {
                    var productTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeData,options);
                    foreach(var item in productTypes)
                    {
                        context.ProductTypes.Add(item);
                    }   
                    await context.SaveChangesAsync();
                }


                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData,options);
                    foreach(var product in products)
                    {
                        context.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}