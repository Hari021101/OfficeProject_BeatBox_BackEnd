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
            var existingCategories = await context.Categories.ToDictionaryAsync(c => c.Name.ToLower(), c => c);

            if (!existingCategories.ContainsKey("headphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Headphones", Description = "Headphones category" };
                context.Categories.Add(newCat);
                existingCategories["headphones"] = newCat;
            }
            await context.SaveChangesAsync();

            var existingProducts = await context.Products.Select(p => p.Name).ToListAsync();
            var productsToAdd = new List<Product>();

            if (!existingProducts.Contains("Purple"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Purple",
                    Description = "Purple",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "hero_headphones.png",
                    CategoryId = existingCategories["headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "N/A",
                    IsFeatured = true,
                    SoldCount = 150,
                    DeliveryDays = 3
                });
            }

            if (productsToAdd.Any())
            {
                await context.Products.AddRangeAsync(productsToAdd);
                await context.SaveChangesAsync();
                
                foreach(var p in productsToAdd) {
                    var inv = new Inventory { Id = Guid.NewGuid(), ProductId = p.Id, AvailableStock = 100, ReservedStock = 0, WarehouseLocation = "Main", LastUpdated = DateTime.UtcNow };
                    context.Inventories.Add(inv);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
