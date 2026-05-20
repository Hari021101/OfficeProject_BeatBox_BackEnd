using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Seed Categories first if none exist
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Wireless Earbuds", Description = "Premium TWS earbuds with ultra-low latency." },
                    new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Over-Ear Headphones", Description = "Active Noise Cancellation over-ear headphones." },
                    new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Bluetooth Speakers", Description = "High-fidelity rugged Bluetooth speakers." },
                    new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Gaming Headsets", Description = "Surround sound immersive gaming headsets." }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products if none exist
            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rockerz Pro ANC 550",
                        Description = "Ultimate over-ear Active Noise Cancellation headphones with dual-mic ENx technology and premium spatial acoustics.",
                        Price = 1999,
                        DiscountPrice = 7990,
                        StockQuantity = 150,
                        ImageUrl = "hero_headphones.png",
                        CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Brand = "BeatBox",
                        Rating = 4.9,
                        BatteryLife = "60 Hours",
                        Color = "Purple",
                        Connectivity = "Wireless",
                        IsFeatured = true
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Airdopes Cyber 141",
                        Description = "Ultimate low-latency wireless gaming earbuds featuring BEAST mode with 40ms low audio latency.",
                        Price = 1299,
                        DiscountPrice = 4490,
                        StockQuantity = 240,
                        ImageUrl = "hero_earbuds.png",
                        CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Brand = "BeatBox",
                        Rating = 4.8,
                        BatteryLife = "42 Hours",
                        Color = "Cyan",
                        Connectivity = "Wireless",
                        IsFeatured = true
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Stone Beat Beast 1200",
                        Description = "High-fidelity rugged Bluetooth speaker throwing 14W signature room-filling bass sound.",
                        Price = 2499,
                        DiscountPrice = 6990,
                        StockQuantity = 80,
                        ImageUrl = "hero_speaker.png",
                        CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        Brand = "BeatBox",
                        Rating = 4.7,
                        BatteryLife = "12 Hours",
                        Color = "Carbon",
                        Connectivity = "Wireless",
                        IsFeatured = true
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Immortal Gaming Pro",
                        Description = "Virtual 7.1 surround sound immersive gaming headset featuring breathing LED lights and high-sensitivity mic.",
                        Price = 1599,
                        DiscountPrice = 4999,
                        StockQuantity = 110,
                        ImageUrl = "hero_headphones.png",
                        CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                        Brand = "BeatBox",
                        Rating = 4.9,
                        BatteryLife = "30 Hours",
                        Color = "Neon",
                        Connectivity = "Wireless",
                        IsFeatured = true
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
