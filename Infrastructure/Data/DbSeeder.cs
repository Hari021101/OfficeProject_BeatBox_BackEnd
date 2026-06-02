using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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

            // Seed Inventory for existing products if missing
            var productsList = await context.Products.ToListAsync();
            foreach (var product in productsList)
            {
                var existingInv = await context.Inventories.FirstOrDefaultAsync(i => i.ProductId == product.Id);
                if (existingInv == null)
                {
                    var inv = new Inventory
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        AvailableStock = Math.Max(0, product.StockQuantity),
                        ReservedStock = 0,
                        WarehouseLocation = "Main Warehouse",
                        LowStockThreshold = 5,
                        LastUpdated = DateTime.UtcNow
                    };

                    await context.Inventories.AddAsync(inv);
                    await context.SaveChangesAsync();

                    await context.InventoryHistories.AddAsync(new InventoryHistory
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = inv.Id,
                        Change = inv.AvailableStock,
                        Reason = "InitialSeed",
                        Timestamp = DateTime.UtcNow,
                        PerformedBy = "system"
                    });

                    await context.SaveChangesAsync();
                }
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

            // Ensure new Categories exist
            var smartWatchCat = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Smart Watches");
            if (smartWatchCat == null)
            {
                smartWatchCat = new Category { Id = Guid.NewGuid(), Name = "Smart Watches", Description = "Next-gen fitness and connectivity wearables." };
                await context.Categories.AddAsync(smartWatchCat);
            }

            var wiredCat = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Wired Headphones");
            if (wiredCat == null)
            {
                wiredCat = new Category { Id = Guid.NewGuid(), Name = "Wired Headphones", Description = "Audiophile grade wired listening experience." };
                await context.Categories.AddAsync(wiredCat);
            }

            await context.SaveChangesAsync();

            // Check if we need to add the new products
            if (!await context.Products.AnyAsync(p => p.Name == "CyberWatch X1"))
            {
                var newProducts = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "CyberWatch X1",
                        Description = "Premium futuristic smartwatch with a glowing neon interface, health tracking, and 7-day battery life.",
                        Price = 3499,
                        DiscountPrice = 8990,
                        StockQuantity = 50,
                        ImageUrl = "hero_smartwatch.png",
                        CategoryId = smartWatchCat.Id,
                        Brand = "BeatBox",
                        Rating = 4.8,
                        BatteryLife = "168 Hours",
                        Color = "Obsidian",
                        Connectivity = "Bluetooth 5.3",
                        IsFeatured = true
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Studio Pro Wired",
                        Description = "Audiophile-grade wired over-ear headphones with a braided cable and ultra-high fidelity drivers.",
                        Price = 4999,
                        DiscountPrice = 12999,
                        StockQuantity = 30,
                        ImageUrl = "hero_wired.png",
                        CategoryId = wiredCat.Id,
                        Brand = "BeatBox",
                        Rating = 5.0,
                        BatteryLife = "Infinite",
                        Color = "Silver/Black",
                        Connectivity = "Wired (3.5mm)",
                        IsFeatured = true
                    }
                };

                await context.Products.AddRangeAsync(newProducts);
                await context.SaveChangesAsync();
            }

            // --- Admin Role & User Seeding ---
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminEmail = "vikram.admin@beatbox.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Vikram Singh (Admin)",
                    IsEmailVerified = true,
                    IsPhoneVerified = true
                };

                var createResult = await userManager.CreateAsync(newAdmin, "Admin@123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(existingAdmin, "Admin"))
                {
                    await userManager.AddToRoleAsync(existingAdmin, "Admin");
                }
            }
        }
    }
}
