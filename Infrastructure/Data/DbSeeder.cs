using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await MockProductsSeeder.SeedAsync(context);
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

            if (!await context.Coupons.AnyAsync())
            {
                await context.Coupons.AddRangeAsync(

                    new Coupon
                    {
                        Code = "WELCOME10",
                        DiscountPercentage = 10,
                        MinimumOrderAmount = 1000,
                        ExpiryDate = DateTime.UtcNow.AddYears(1),
                        IsActive = true,
                        UsageLimit = 1000
                    },

                    new Coupon
                    {
                        Code = "BEATBOX500",
                        DiscountAmount = 500,
                        MinimumOrderAmount = 3000,
                        ExpiryDate = DateTime.UtcNow.AddYears(1),
                        IsActive = true,
                        UsageLimit = 500
                    },

                    new Coupon
                    {
                        Code = "SUMMER20",
                        DiscountPercentage = 20,
                        MinimumOrderAmount = 5000,
                        ExpiryDate = DateTime.UtcNow.AddMonths(3),
                        IsActive = true,
                        UsageLimit = 100
                    }
                );

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

            // =============================================
            // PHASE 1 MIGRATION: Audio Categories & Products
            // =============================================

            // Ensure all Audio subcategories exist
            var audioCategories = new Dictionary<string, string>
            {
                { "Soundbars",             "Premium soundbars for home theatre." },
                { "Party Speakers",        "High-wattage speakers for parties and events." },
                { "Portable Speakers",     "Compact wireless speakers for on-the-go." },
                { "TWS Earbuds",           "True Wireless Stereo earbuds." },
                { "Neckbands",             "Wireless neckband earphones." },
                { "Wireless Headphones",   "Over-ear and on-ear wireless headphones." },
                { "Wired Earphones",       "High-fidelity wired in-ear monitors." },
                { "USB Speakers",          "USB-powered desktop speakers." },
                { "Conference Speakers",   "Speakerphones for meetings and calls." },
                { "Wireless Microphones",  "Professional wireless mic systems." },
            };

            var audioCatEntities = new Dictionary<string, Category>();
            foreach (var kv in audioCategories)
            {
                var cat = await context.Categories.FirstOrDefaultAsync(c => c.Name == kv.Key);
                if (cat == null)
                {
                    cat = new Category { Id = Guid.NewGuid(), Name = kv.Key, Description = kv.Value };
                    await context.Categories.AddAsync(cat);
                    await context.SaveChangesAsync();
                }
                audioCatEntities[kv.Key] = cat;
            }

            // Helper: add product if not already present
            async Task AddProductIfMissing(string name, string description, decimal price, decimal discountPrice,
                int stock, string imageUrl, Guid categoryId, string brand, double rating,
                string batteryLife, string color, string connectivity)
            {
                if (!await context.Products.AnyAsync(p => p.Name == name))
                {
                    var p = new Product
                    {
                        Id = Guid.NewGuid(), Name = name, Description = description,
                        Price = price, DiscountPrice = discountPrice, StockQuantity = stock,
                        ImageUrl = imageUrl, CategoryId = categoryId, Brand = brand,
                        Rating = rating, BatteryLife = batteryLife, Color = color,
                        Connectivity = connectivity, IsFeatured = true,
                        SoldCount = new Random().Next(50, 500), DeliveryDays = 3
                    };
                    await context.Products.AddAsync(p);
                    await context.SaveChangesAsync();
                }
            }

            // Soundbars
            await AddProductIfMissing("BeatBox Soundbar 2.1", "2.1 channel soundbar with wireless subwoofer.", 4999, 2999, 80, "soundbar.png", audioCatEntities["Soundbars"].Id, "BeatBox", 4.7, "N/A", "Black", "HDMI ARC / Optical");
            await AddProductIfMissing("BeatBox Soundbar Pro 5.1", "Immersive 5.1 surround soundbar, Dolby Audio.", 8999, 6999, 50, "soundbar.png", audioCatEntities["Soundbars"].Id, "BeatBox", 4.8, "N/A", "Black", "HDMI ARC");
            await AddProductIfMissing("BeatBox Gaming Soundbar X", "Gaming soundbar with Virtual 7.1 and low latency.", 4999, 3499, 60, "soundbar.png", audioCatEntities["Soundbars"].Id, "BeatBox", 4.7, "N/A", "Black", "Bluetooth / Optical");

            // Party Speakers
            await AddProductIfMissing("Party Boom 1500", "1500W peak party speaker with disco lights and mic input.", 12999, 9999, 30, "party_speaker.png", audioCatEntities["Party Speakers"].Id, "BeatBox", 4.8, "8 Hours", "Black", "Bluetooth 5.0");
            await AddProductIfMissing("Party Blast Tower", "Tower party speaker with karaoke mic and FM radio.", 14999, 11999, 20, "party_speaker.png", audioCatEntities["Party Speakers"].Id, "BeatBox", 4.9, "N/A", "Black", "Bluetooth / AUX");
            await AddProductIfMissing("Party Lite Wireless", "Portable party speaker, IPX5, 12-hour battery.", 7999, 5999, 60, "party_speaker.png", audioCatEntities["Party Speakers"].Id, "BeatBox", 4.6, "12 Hours", "Black", "Bluetooth 5.0");

            // Portable Speakers
            await AddProductIfMissing("Portable Rugged X3", "IP67 waterproof rugged Bluetooth speaker, 24-hour battery.", 2499, 1799, 120, "hero_speaker.png", audioCatEntities["Portable Speakers"].Id, "BeatBox", 4.8, "24 Hours", "Green", "Bluetooth 5.0");
            await AddProductIfMissing("Portable Bass Booster", "360° passive bass radiator, TWS pairable.", 1999, 1499, 150, "hero_speaker.png", audioCatEntities["Portable Speakers"].Id, "BeatBox", 4.7, "16 Hours", "Black", "Bluetooth 5.0");
            await AddProductIfMissing("Pocket Mini Speaker", "Ultra-compact pocket speaker, USB-C charging.", 999, 699, 200, "hero_speaker.png", audioCatEntities["Portable Speakers"].Id, "BeatBox", 4.5, "8 Hours", "Blue", "Bluetooth 5.0");

            // TWS Earbuds
            await AddProductIfMissing("TWS ANC Elite", "Hybrid ANC TWS earbuds, 40-hour total playback.", 3499, 2499, 100, "smart_earbuds.png", audioCatEntities["TWS Earbuds"].Id, "BeatBox", 4.9, "40 Hours", "White", "Bluetooth 5.3");
            await AddProductIfMissing("TWS Sport Pro", "IPX5 sport TWS with ear hooks, 36-hour battery.", 1999, 1499, 130, "smart_earbuds.png", audioCatEntities["TWS Earbuds"].Id, "BeatBox", 4.7, "36 Hours", "Black", "Bluetooth 5.0");
            await AddProductIfMissing("TWS Lite Everyday", "Best-value TWS, 24-hour battery, touch controls.", 799, 599, 250, "smart_earbuds.png", audioCatEntities["TWS Earbuds"].Id, "BeatBox", 4.5, "24 Hours", "Black", "Bluetooth 5.0");

            // Neckbands
            await AddProductIfMissing("Neckband Pro ANC", "Active noise cancellation neckband, 30-hour playback.", 1499, 999, 140, "wireless_neckband.png", audioCatEntities["Neckbands"].Id, "BeatBox", 4.8, "30 Hours", "Black", "Bluetooth 5.0");
            await AddProductIfMissing("Neckband Sport Flex", "Memory-flex band, IPX4 rated, 28-hour battery.", 1299, 799, 160, "wireless_neckband.png", audioCatEntities["Neckbands"].Id, "BeatBox", 4.7, "28 Hours", "Blue", "Bluetooth 5.0");

            // Wireless Headphones
            await AddProductIfMissing("ANC Headphones Pro", "45dB ANC, premium leather cushions, 50-hour playback.", 4999, 2999, 70, "hero_headphones.png", audioCatEntities["Wireless Headphones"].Id, "BeatBox", 4.9, "50 Hours", "Black", "Bluetooth 5.2");
            await AddProductIfMissing("Wireless Headphones Lite", "Lightweight foldable, 40-hour playback, best value.", 1999, 1499, 120, "hero_headphones.png", audioCatEntities["Wireless Headphones"].Id, "BeatBox", 4.6, "40 Hours", "White", "Bluetooth 5.0");
            await AddProductIfMissing("Studio Wireless Headphones X", "Studio-grade flat EQ, 50mm drivers, for creators.", 6999, 4999, 40, "hero_headphones.png", audioCatEntities["Wireless Headphones"].Id, "BeatBox", 4.9, "30 Hours", "Silver", "Bluetooth 5.2");

            // Wired Earphones
            await AddProductIfMissing("Wired Pro IEM", "Balanced armature driver, braided cable, audiophile grade.", 799, 499, 180, "wired_earphones.png", audioCatEntities["Wired Earphones"].Id, "BeatBox", 4.8, "N/A", "Silver", "Wired 3.5mm");
            await AddProductIfMissing("Wired Bass Boost", "12mm deep-bass driver, in-line mic.", 499, 299, 250, "wired_earphones.png", audioCatEntities["Wired Earphones"].Id, "BeatBox", 4.6, "N/A", "Black", "Wired 3.5mm");
            await AddProductIfMissing("Type-C Wired Earphones", "USB-C with built-in DAC, Hi-Res audio certified.", 899, 599, 130, "wired_earphones.png", audioCatEntities["Wired Earphones"].Id, "BeatBox", 4.7, "N/A", "Black", "Wired USB-C");

            // USB Speakers
            await AddProductIfMissing("USB RGB Gaming Speakers", "10W RMS, RGB lighting, headphone jack.", 1299, 899, 100, "usb_speakers.png", audioCatEntities["USB Speakers"].Id, "BeatBox", 4.7, "N/A", "Black", "USB");
            await AddProductIfMissing("USB Mini Desktop Speakers", "5W bus-powered, no adapter needed, volume knob.", 699, 449, 150, "usb_speakers.png", audioCatEntities["USB Speakers"].Id, "BeatBox", 4.5, "N/A", "Black", "USB");
            await AddProductIfMissing("USB Desktop Soundbar", "15W slim under-monitor soundbar, optical input.", 1499, 999, 80, "usb_speakers.png", audioCatEntities["USB Speakers"].Id, "BeatBox", 4.6, "N/A", "Black", "USB / Optical");

            // Conference Speakers
            await AddProductIfMissing("Conference Speaker 360", "6-mic array, 360° pickup, echo cancellation.", 4999, 3499, 40, "conference_speakers.png", audioCatEntities["Conference Speakers"].Id, "BeatBox", 4.8, "N/A", "Black", "USB / Bluetooth");
            await AddProductIfMissing("Portable Conference Speaker", "300g travel speakerphone, 10-hour battery.", 2999, 1999, 60, "conference_speakers.png", audioCatEntities["Conference Speakers"].Id, "BeatBox", 4.6, "10 Hours", "Grey", "USB-C / Bluetooth");
            await AddProductIfMissing("Conference Elite Hub", "AI noise cancellation, 8-mic array.", 7999, 5999, 25, "conference_speakers.png", audioCatEntities["Conference Speakers"].Id, "BeatBox", 4.9, "N/A", "Black", "USB");

            // Wireless Microphones
            await AddProductIfMissing("Wireless Lavalier Clip Mic", "Clip-on vlogging mic, 20ms latency, noise shield.", 3499, 2499, 60, "wireless_microphones.png", audioCatEntities["Wireless Microphones"].Id, "BeatBox", 4.7, "8 Hours", "Black", "2.4GHz");
            await AddProductIfMissing("Wireless Handheld Mic", "Stage-ready, 80m range, anti-drop design.", 5999, 3999, 45, "wireless_microphones.png", audioCatEntities["Wireless Microphones"].Id, "BeatBox", 4.8, "10 Hours", "Black", "UHF Wireless");
            await AddProductIfMissing("Wireless Dual Mic System", "Dual channel, 100m range, mixer output.", 8999, 6499, 25, "wireless_microphones.png", audioCatEntities["Wireless Microphones"].Id, "BeatBox", 4.9, "8 Hours/Mic", "Black", "UHF Dual Channel");

            // =============================================
            // PHASE 2 MIGRATION: Computer, Car & Smart Gadgets
            // =============================================

            var phase2Categories = new Dictionary<string, string>
            {
                { "Computer Accessories", "Keyboards, mice, and desk setups." },
                { "Car Accessories",      "Chargers, inflators, and car care." },
                { "Smart Gadgets",        "Trackers, fans, and everyday tech." }
            };

            var phase2CatEntities = new Dictionary<string, Category>();
            foreach (var kv in phase2Categories)
            {
                var cat = await context.Categories.FirstOrDefaultAsync(c => c.Name == kv.Key);
                if (cat == null)
                {
                    cat = new Category { Id = Guid.NewGuid(), Name = kv.Key, Description = kv.Value };
                    await context.Categories.AddAsync(cat);
                    await context.SaveChangesAsync();
                }
                phase2CatEntities[kv.Key] = cat;
            }

            // Computer Accessories
            await AddProductIfMissing("Pro Wireless Keyboard", "Low-profile mechanical wireless keyboard.", 4999, 3499, 100, "gaming_keyboard.png", phase2CatEntities["Computer Accessories"].Id, "BeatBox", 4.8, "200 Hours", "Black", "Wireless");
            await AddProductIfMissing("Ergo Master Mouse", "Ergonomic wireless mouse with multi-device support.", 2999, 1999, 120, "gaming_mouse.png", phase2CatEntities["Computer Accessories"].Id, "BeatBox", 4.7, "100 Hours", "Grey", "Wireless");
            await AddProductIfMissing("Alloy Laptop Stand", "Premium aluminum adjustable laptop stand.", 1999, 1299, 150, "laptop_stand.png", phase2CatEntities["Computer Accessories"].Id, "BeatBox", 4.9, "N/A", "Silver", "N/A");

            // Car Accessories
            await AddProductIfMissing("Dual Port Car Charger", "65W fast charging dual port car charger.", 1299, 799, 200, "car_charger.png", phase2CatEntities["Car Accessories"].Id, "BeatBox", 4.6, "N/A", "Black", "N/A");
            await AddProductIfMissing("Smart Tyre Inflator", "Portable cordless tyre inflator with digital display.", 3499, 2499, 80, "tyre_inflator.png", phase2CatEntities["Car Accessories"].Id, "BeatBox", 4.8, "N/A", "Black", "N/A");
            await AddProductIfMissing("Handheld Car Vacuum", "High-power cordless vacuum cleaner for cars.", 2999, 1999, 90, "vacuum_cleaner.png", phase2CatEntities["Car Accessories"].Id, "BeatBox", 4.7, "N/A", "Black", "N/A");

            // Smart Gadgets
            await AddProductIfMissing("Smart Location Tracker", "Bluetooth item finder with anti-lost alarm.", 999, 699, 250, "smart_tracker.png", phase2CatEntities["Smart Gadgets"].Id, "BeatBox", 4.5, "1 Year", "White", "Bluetooth");
            await AddProductIfMissing("Portable Neck Fan", "Bladeless neck fan with 3 speed modes.", 1499, 999, 150, "portable_fan.png", phase2CatEntities["Smart Gadgets"].Id, "BeatBox", 4.6, "12 Hours", "White", "N/A");
            await AddProductIfMissing("Pro Grooming Trimmer", "Cordless beard and hair trimmer with precision dial.", 1999, 1299, 110, "trimmer.png", phase2CatEntities["Smart Gadgets"].Id, "BeatBox", 4.7, "90 Mins", "Black", "N/A");

        }
    }
}
