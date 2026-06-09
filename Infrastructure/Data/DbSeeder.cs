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
                        Price = 7999,
                        DiscountPrice = 1990,
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
                        Price = 4299,
                        DiscountPrice = 1490,
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
                        Price = 6499,
                        DiscountPrice = 2990,
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
                        Price = 4599,
                        DiscountPrice = 1999,
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
            if (!await context.Products.AnyAsync(p => p.Name == "BeatBox Thunder ANC 900"))
            {
                await context.Products.AddRangeAsync(
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "BeatBox Thunder ANC 900",
                        Description = "Flagship noise-cancelling headphones.",
                        Price = 9999,
                        DiscountPrice = 7499,
                        StockQuantity = 60,
                        ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e",
                        CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Brand = "BeatBox",
                        Rating = 4.8,
                        BatteryLife = "70 Hours",
                        Color = "Silver",
                        Connectivity = "Wireless",
                        IsFeatured = true,
                        SoldCount = 250,
                        DeliveryDays = 3
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "BeatBox Party Blast 500",
                        Description = "Portable party speaker with RGB lights.",
                        Price = 7999,
                        DiscountPrice = 5999,
                        StockQuantity = 80,
                        ImageUrl = "https://images.unsplash.com/photo-1589003077984-894e133dabab",
                        CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        Brand = "BeatBox",
                        Rating = 4.7,
                        BatteryLife = "18 Hours",
                        Color = "Black",
                        Connectivity = "Bluetooth",
                        IsFeatured = true,
                        SoldCount = 190,
                        DeliveryDays = 2
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "BeatBox Titan RGB Pro",
                        Description = "Professional gaming headset with RGB effects.",
                        Price = 4999,
                        DiscountPrice = 3499,
                        StockQuantity = 3,
                        ImageUrl = "https://images.unsplash.com/photo-1599669454699-248893623440",
                        CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                        Brand = "BeatBox",
                        Rating = 4.9,
                        BatteryLife = "40 Hours",
                        Color = "Red",
                        Connectivity = "Wireless",
                        IsFeatured = true,
                        SoldCount = 430,
                        DeliveryDays = 1
                    }
                );

                await context.SaveChangesAsync();
            }
            // Seed Product Images, FAQs and Reviews
            if (!await context.ProductImages.AnyAsync())
            {
                var products = await context.Products.ToListAsync();

                foreach (var product in products)
                {
                    switch (product.Name)
                    {
                        case "Rockerz Pro ANC 550":

                            await context.ProductImages.AddRangeAsync(

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e",
                                    ColorName = "Black",
                                    ColorCode = "#111111",
                                    IsPrimary = true
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1484704849700-f032a568e944",
                                    ColorName = "Purple",
                                    ColorCode = "#7C3AED",
                                    IsPrimary = false
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b",
                                    ColorName = "Silver",
                                    ColorCode = "#D1D5DB",
                                    IsPrimary = false
                                }

                            );

                            break;

                        case "Airdopes Cyber 141":

                            await context.ProductImages.AddRangeAsync(

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1590658268037-6bf12165a8df",
                                    ColorName = "Black",
                                    ColorCode = "#111111",
                                    IsPrimary = true
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1606220588913-b3aacb4d2f46",
                                    ColorName = "White",
                                    ColorCode = "#FFFFFF",
                                    IsPrimary = false
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1572569511254-d8f925fe2cbb",
                                    ColorName = "Cyan",
                                    ColorCode = "#06B6D4",
                                    IsPrimary = false
                                }

                            );

                            break;

                        case "Stone Beat Beast 1200":

                            await context.ProductImages.AddRangeAsync(

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1589003077984-894e133dabab",
                                    ColorName = "Black",
                                    ColorCode = "#111111",
                                    IsPrimary = true
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1",
                                    ColorName = "Blue",
                                    ColorCode = "#2563EB",
                                    IsPrimary = false
                                }

                            );

                            break;

                        case "Immortal Gaming Pro":

                            await context.ProductImages.AddRangeAsync(

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1599669454699-248893623440",
                                    ColorName = "Black",
                                    ColorCode = "#111111",
                                    IsPrimary = true
                                },

                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b",
                                    ColorName = "Red",
                                    ColorCode = "#DC2626",
                                    IsPrimary = false
                                }

                            );

                            break;

                        default:

                            await context.ProductImages.AddAsync(
                                new ProductImage
                                {
                                    ProductId = product.Id,
                                    ImageUrl = product.ImageUrl,
                                    ColorName = product.Color,
                                    ColorCode = "#111111",
                                    IsPrimary = true
                                });

                            break;
                    }
                }

                await context.SaveChangesAsync();
            }
            if (!await context.ProductFaqs.AnyAsync())
            {
                var products = await context.Products.ToListAsync();

                foreach (var product in products)
                {
                    await context.ProductFaqs.AddRangeAsync(
                        new ProductFaq
                        {
                            ProductId = product.Id,
                            Question = "What is the warranty period?",
                            Answer = "All BeatBox products come with 1 year warranty."
                        },
                        new ProductFaq
                        {
                            ProductId = product.Id,
                            Question = "Does it support fast charging?",
                            Answer = "Yes, fast charging is supported."
                        },
                        new ProductFaq
                        {
                            ProductId = product.Id,
                            Question = "Can I return this product?",
                            Answer = "Yes, within 7 days of delivery."
                        }
                    );
                }

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
                        Price = 8499,
                        DiscountPrice = 3990,
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
                        Price = 14999,
                        DiscountPrice = 2999,
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

            if (!await context.ProductReviews.AnyAsync())
            {
                var admin = await userManager.FindByEmailAsync("vikram.admin@beatbox.com");

                if (admin != null)
                {
                    var products = await context.Products.ToListAsync();

                    foreach (var product in products)
                    {
                        await context.ProductReviews.AddRangeAsync(
                            new ProductReview
                            {
                                ProductId = product.Id,
                                UserId = admin.Id,
                                Rating = 5,
                                Comment = "Amazing sound quality and premium build.",
                                CreatedDate = DateTime.UtcNow.AddDays(-10),
                                IsVerifiedPurchase = true
                            },
                            new ProductReview
                            {
                                ProductId = product.Id,
                                UserId = admin.Id,
                                Rating = 4,
                                Comment = "Battery backup is excellent.",
                                CreatedDate = DateTime.UtcNow.AddDays(-5),
                                IsVerifiedPurchase = true
                            },
                            new ProductReview
                            {
                                ProductId = product.Id,
                                UserId = admin.Id,
                                Rating = 5,
                                Comment = "Worth every rupee.",
                                CreatedDate = DateTime.UtcNow.AddDays(-2),
                                IsVerifiedPurchase = true
                            }
                        );
                    }

                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
