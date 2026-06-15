using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class MockProductsSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var categoriesList = new List<Category>();
            var existingCategories = await context.Categories.ToDictionaryAsync(c => c.Name.ToLower(), c => c);
            await context.SaveChangesAsync();

            var existingProducts = await context.Products.Select(p => p.Name).ToListAsync();
            var productsToAdd = new List<Product>();

            if (productsToAdd.Any())
            {
                await context.Products.AddRangeAsync(productsToAdd);
                await context.SaveChangesAsync();
                
                // Add Inventory for them
                foreach(var p in productsToAdd) {
                    var inv = new Inventory { Id = Guid.NewGuid(), ProductId = p.Id, AvailableStock = 100, ReservedStock = 0, WarehouseLocation = "Main", LastUpdated = DateTime.UtcNow };
                    context.Inventories.Add(inv);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
