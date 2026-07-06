using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public static class DbSeeder
{
    private const string ImageBasePath = "/images/products";
    private const string PlaceholderImagePath = "/images/products/placeholder.jpg";

    private static Dictionary<string, string[]> CategoryImages = new(StringComparer.OrdinalIgnoreCase);
    private static string _contentRootPath = "";

    public static readonly string[] SeededCategories = new[]
    {
        "True Wireless Earbuds",
        "Neckbands",
        "Wireless Headphones",
        "Wired Earphones",
        "Bluetooth Speakers",
        "Soundbars",
        "Home Audio",
        "Party Speakers",
        "Gaming Headsets",
        "Smart Watches",
        "Keyboards",
        "Wireless Mouse"
    };

    private static readonly Dictionary<string, string> CategoryToFolderMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "True Wireless Earbuds", "tws_eaebuds" },
        { "Neckbands", "neckbands" },
        { "Wireless Headphones", "headphone" },
        { "Wired Earphones", "wired_headphones" },
        { "Bluetooth Speakers", "bluetooth_speaker" },
        { "Soundbars", "soundbars" },
        { "Home Audio", "usb-speakers" },
        { "Party Speakers", "party-speakers" },
        { "Gaming Headsets", "wireless-headphones" },
        { "Smart Watches", "default" },
        { "Keyboards", "keyboards" },
        { "Wireless Mouse", "mouse" }
    };

    private class ProductTemplate
    {
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BatteryLife { get; set; } = string.Empty;
        public string Connectivity { get; set; } = string.Empty;
        public List<VariantTemplate> Variants { get; set; } = new();
    }

    private class VariantTemplate
    {
        public string Color { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImageFilter { get; set; } = string.Empty;
    }

    private static readonly List<ProductTemplate> ProductTemplates = new()
    {
        // 1. True Wireless Earbuds
        new ProductTemplate
        {
            CategoryName = "True Wireless Earbuds",
            Name = "BeatBox AirBuds 100",
            Brand = "BeatBox",
            Description = "Experience true audio freedom with the BeatBox AirBuds 100. Featuring high-fidelity drivers, ultra-low latency gaming mode, and ergonomic comfort for all-day listening.",
            BatteryLife = "40 Hours",
            Connectivity = "Bluetooth 5.3",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-AB100-BLK", Price = 2999, DiscountPrice = 1299, ImageFilter = "tws1_black" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BB-AB100-WHT", Price = 2999, DiscountPrice = 1299, ImageFilter = "tws1_white" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "True Wireless Earbuds",
            Name = "JBL C105TWS",
            Brand = "JBL",
            Description = "Immerse yourself in JBL Signature Sound. The C105TWS delivers pure bass performance, hands-free stereo calling, and a compact charging case that fits perfectly in your pocket.",
            BatteryLife = "17 Hours",
            Connectivity = "Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "JBL-C105-BLK", Price = 7999, DiscountPrice = 3499, ImageFilter = "tws2_black" },
                new VariantTemplate { Color = "Blue", ColorCode = "#2563EB", Sku = "JBL-C105-BLU", Price = 7999, DiscountPrice = 3499, ImageFilter = "tws2_blue" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "True Wireless Earbuds",
            Name = "boAt Airdopes 131",
            Brand = "boAt",
            Description = "Bring your favorite playlist to life with boAt Airdopes 131. Designed with Insta Wake 'n' Pair technology, it pairs automatically the moment you open the case lid.",
            BatteryLife = "15 Hours",
            Connectivity = "Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Grey", ColorCode = "#6B7280", Sku = "BOAT-AD131-GRY", Price = 2990, DiscountPrice = 999, ImageFilter = "tws3_grey" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BOAT-AD131-WHT", Price = 2990, DiscountPrice = 999, ImageFilter = "tws3_white" }
            }
        },

        // 2. Wireless Headphones
        new ProductTemplate
        {
            CategoryName = "Wireless Headphones",
            Name = "BeatBox Studio Pro",
            Brand = "BeatBox",
            Description = "Uncompromising studio-grade sound meets wireless convenience. The BeatBox Studio Pro features Hybrid Active Noise Cancellation, high-res audio drivers, and plush memory foam earcups.",
            BatteryLife = "50 Hours",
            Connectivity = "Bluetooth 5.3",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-SP-BLK", Price = 14999, DiscountPrice = 6999, ImageFilter = "headphone1_black" },
                new VariantTemplate { Color = "Brown", ColorCode = "#8B4513", Sku = "BB-SP-BRN", Price = 14999, DiscountPrice = 6999, ImageFilter = "headphone1_brown" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "Wireless Headphones",
            Name = "JBL Tune 510BT",
            Brand = "JBL",
            Description = "Grab a pair of JBL Tune 510BT and stream powerful JBL Pure Bass sound with no strings attached. Lightweight, comfortable, and folds flat for easy transport.",
            BatteryLife = "40 Hours",
            Connectivity = "Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Red", ColorCode = "#DC2626", Sku = "JBL-T510-RED", Price = 4499, DiscountPrice = 2899, ImageFilter = "headphone2_red" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "JBL-T510-WHT", Price = 4499, DiscountPrice = 2899, ImageFilter = "headphone2_white" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "Wireless Headphones",
            Name = "boAt Rockerz 550",
            Brand = "boAt",
            Description = "Designed for music lovers, the boAt Rockerz 550 features 50mm dynamic drivers that deliver punchy bass and crystalline vocals, alongside an ergonomic over-ear design.",
            BatteryLife = "20 Hours",
            Connectivity = "Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Orange", ColorCode = "#EA580C", Sku = "BOAT-R550-ORG", Price = 4999, DiscountPrice = 1999, ImageFilter = "headphone3_orange" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BOAT-R550-WHT", Price = 4999, DiscountPrice = 1999, ImageFilter = "headphone3_white" }
            }
        },

        // 3. Bluetooth Speakers
        new ProductTemplate
        {
            CategoryName = "Bluetooth Speakers",
            Name = "BeatBox BoomCan 2.0",
            Brand = "BeatBox",
            Description = "Make way for heavy sound in a mini container. The BeatBox BoomCan 2.0 is a pocket-sized Bluetooth speaker with punchy acoustics and rugged IPX7 waterproof durability.",
            BatteryLife = "10 Hours",
            Connectivity = "Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-BC2-BLK", Price = 3999, DiscountPrice = 1499, ImageFilter = "bluetoothSpeaker2_black" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BB-BC2-WHT", Price = 3999, DiscountPrice = 1499, ImageFilter = "bluetoothSpeaker2_white" },
                new VariantTemplate { Color = "Brown", ColorCode = "#8B4513", Sku = "BB-BC2-BRN", Price = 3999, DiscountPrice = 1499, ImageFilter = "bluetoothSpeaker2_brown" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "Bluetooth Speakers",
            Name = "JBL Flip 6",
            Brand = "JBL",
            Description = "The JBL Flip 6 2-way speaker system delivers loud, crystal-clear, powerful sound. Ruggedly designed, it is IP67 waterproof and dustproof, ready to go anywhere.",
            BatteryLife = "12 Hours",
            Connectivity = "Bluetooth 5.1",
            Variants = new()
            {
                new VariantTemplate { Color = "Grey", ColorCode = "#6B7280", Sku = "JBL-F6-GRY", Price = 11999, DiscountPrice = 9999, ImageFilter = "bluetoothSpeaker3_grey" },
                new VariantTemplate { Color = "Red", ColorCode = "#DC2626", Sku = "JBL-F6-RED", Price = 11999, DiscountPrice = 9999, ImageFilter = "bluetoothSpeaker3_red" }
            }
        },

        // 4. Wired Earphones
        new ProductTemplate
        {
            CategoryName = "Wired Earphones",
            Name = "BeatBox BassHeads 100",
            Brand = "BeatBox",
            Description = "Get the famous hawk-inspired acoustic sound. The BassHeads 100 feature a premium coated cable, built-in HD microphone, and a stylish ergonomic shell.",
            BatteryLife = "N/A",
            Connectivity = "3.5mm Jack",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-BH100-BLK", Price = 999, DiscountPrice = 399, ImageFilter = "wired_earPhones1_black" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BB-BH100-WHT", Price = 999, DiscountPrice = 399, ImageFilter = "wired_earPhones1_white" }
            }
        },
        new ProductTemplate
        {
            CategoryName = "Wired Earphones",
            Name = "boAt BassHeads 225",
            Brand = "boAt",
            Description = "Designed for style and bass performance. The BassHeads 225 feature polished metal chambers, dynamic 10mm drivers, and flat tangle-resistant cables.",
            BatteryLife = "N/A",
            Connectivity = "3.5mm Jack",
            Variants = new()
            {
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BOAT-BH225-WHT", Price = 999, DiscountPrice = 499, ImageFilter = "wired_earPhones2_white" }
            }
        },

        // 7. Party Speakers
        new ProductTemplate
        {
            CategoryName = "Party Speakers",
            Name = "BeatBox Party Blast",
            Brand = "BeatBox",
            Description = "Power up your celebrations with BeatBox Party Blast. Offering robust multi-directional sound, wireless karaoke mic, and multi-color beats-locked flashing lights.",
            BatteryLife = "8 Hours",
            Connectivity = "Bluetooth 5.0, AUX, SD Card",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-PB-BLK", Price = 14999, DiscountPrice = 7999, ImageFilter = "generic" }
            }
        },

       
        // 9. Gaming Headsets
        new ProductTemplate
        {
            CategoryName = "Gaming Headsets",
            Name = "JBL Quantum 100",
            Brand = "JBL",
            Description = "Turn your game into an epic event. The JBL Quantum 100 features QuantumSOUND Signature that puts you in the center of the action with realistic soundscapes.",
            BatteryLife = "N/A",
            Connectivity = "3.5mm Audio Cable",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "JBL-Q100-BLK", Price = 3999, DiscountPrice = 2499, ImageFilter = "generic" },
                new VariantTemplate { Color = "Blue", ColorCode = "#2563EB", Sku = "JBL-Q100-BLU", Price = 3999, DiscountPrice = 2499, ImageFilter = "generic" }
            }
        },

        // 10. Smart Watches
        new ProductTemplate
        {
            CategoryName = "Smart Watches",
            Name = "BeatBox SmartWatch Active",
            Brand = "BeatBox",
            Description = "Stay connected and track your health metrics in real-time. Features a premium metal dial, bright AMOLED screen, multi-sport tracking, and 24/7 heart rate monitoring.",
            BatteryLife = "7 Days",
            Connectivity = "Bluetooth 5.1",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-WA-BLK", Price = 5999, DiscountPrice = 2499, ImageFilter = "generic" },
                new VariantTemplate { Color = "Silver", ColorCode = "#C0C0C0", Sku = "BB-WA-SLV", Price = 5999, DiscountPrice = 2499, ImageFilter = "generic" }
            }
        },

        // 11. Keyboards
        new ProductTemplate
        {
            CategoryName = "Keyboards",
            Name = "BeatBox Elite Key",
            Brand = "BeatBox",
            Description = "Type in absolute comfort and silence. The Elite Key is a slim, minimalist wireless keyboard designed for multi-device workflows and comfortable quiet typing.",
            BatteryLife = "6 Months",
            Connectivity = "2.4GHz Wireless, Bluetooth 5.0",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-EK-BLK", Price = 1999, DiscountPrice = 999, ImageFilter = "generic" },
                new VariantTemplate { Color = "White", ColorCode = "#FFFFFF", Sku = "BB-EK-WHT", Price = 1999, DiscountPrice = 999, ImageFilter = "generic" }
            }
        },

        // 12. Wireless Mouse
        new ProductTemplate
        {
            CategoryName = "Wireless Mouse",
            Name = "BeatBox Stealth Click",
            Brand = "BeatBox",
            Description = "Experience noise-free navigation. The Stealth Click features silent micro-switches, adjustable DPI sensitivity, and an ergonomic contour shape that supports your hand.",
            BatteryLife = "12 Months",
            Connectivity = "2.4GHz USB Receiver, Bluetooth",
            Variants = new()
            {
                new VariantTemplate { Color = "Black", ColorCode = "#111111", Sku = "BB-SC-BLK", Price = 999, DiscountPrice = 499, ImageFilter = "generic" },
                new VariantTemplate { Color = "Grey", ColorCode = "#6B7280", Sku = "BB-SC-GRY", Price = 999, DiscountPrice = 499, ImageFilter = "generic" }
            }
        }
    };

    public static void SetContentRootPath(string contentRootPath)
    {
        _contentRootPath = contentRootPath;
    }

    public static void InitializeImagePools()
    {
        var map = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        // Load default images pool
        var defaultImages = LoadCategoryImages("default");
        map["default"] = defaultImages;

        foreach (var categoryName in SeededCategories)
        {
            if (CategoryToFolderMap.TryGetValue(categoryName, out var folder))
            {
                var imgs = LoadCategoryImages(folder);
                if (imgs.Length == 0) imgs = defaultImages;
                map[categoryName] = imgs;
            }
            else
            {
                map[categoryName] = defaultImages;
            }
        }

        CategoryImages = map;
    }

    private static string[] LoadCategoryImages(string categoryFolder)
    {
        if (string.IsNullOrWhiteSpace(_contentRootPath))
        {
            return Array.Empty<string>();
        }

        var folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", categoryFolder);

        if (!Directory.Exists(folderPath))
        {
            folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", "default");
            categoryFolder = "default";
        }

        if (!Directory.Exists(folderPath))
        {
            return Array.Empty<string>();
        }

        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
            .OrderBy(f => f)
            .ToList();

        if (!imageFiles.Any())
        {
            if (categoryFolder != "default")
            {
                return LoadCategoryImages("default");
            }
            return Array.Empty<string>();
        }

        return imageFiles
            .Select(f => $"{ImageBasePath}/{categoryFolder}/{Path.GetFileName(f)}")
            .ToArray();
    }

    private static List<ProductVariantImage> BuildImagesForVariant(
        string categoryName,
        string imageFilter,
        int variantIndex,
        int imagesPerVariant)
    {
        var pool = CategoryImages.TryGetValue(categoryName, out var catPool) ? catPool : CategoryImages["default"];
        if (pool == null || pool.Length == 0)
        {
            pool = CategoryImages["default"];
        }

        List<string> filtered = new();
        if (!string.IsNullOrEmpty(imageFilter) && !imageFilter.Equals("generic", StringComparison.OrdinalIgnoreCase))
        {
            filtered = pool.Where(p => p.Contains(imageFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtered.Count == 0 && imageFilter.Contains('_'))
            {
                var modelPrefix = imageFilter.Split('_')[0];
                filtered = pool.Where(p => p.Contains(modelPrefix, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        if (filtered.Count > 0)
        {
            var result = new List<ProductVariantImage>();
            for (int i = 0; i < filtered.Count; i++)
            {
                result.Add(new ProductVariantImage
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = filtered[i],
                    IsPrimary = i == 0,
                    DisplayOrder = i + 1
                });
            }
            return result;
        }

        var fallbackList = new List<ProductVariantImage>();
        var startIndex = (variantIndex * imagesPerVariant) % pool.Length;
        for (int i = 0; i < imagesPerVariant; i++)
        {
            var idx = (startIndex + i) % pool.Length;
            fallbackList.Add(new ProductVariantImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = pool[idx],
                IsPrimary = i == 0,
                DisplayOrder = i + 1
            });
        }
        return fallbackList;
    }

    public static async Task SeedAsync(
        AppDbContext context, 
        UserManager<AppUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        bool force = false, 
        bool isDevelopment = false)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (userManager == null) throw new ArgumentNullException(nameof(userManager));
        if (roleManager == null) throw new ArgumentNullException(nameof(roleManager));

        // Normal mode safety checks: exit early if database already contains records
        if (!force && (await context.Categories.AnyAsync() || await context.Products.AnyAsync()))
        {
            await EnsureAdminAsync(userManager, roleManager);
            await EnsureRegularUserAsync(userManager, roleManager);

            if (isDevelopment && !await context.Coupons.AnyAsync())
            {
                var coupons = new List<Coupon>
                {
                    new Coupon { Code = "DEAL10", DiscountType = "Percentage", DiscountPercentage = 10m, MinimumOrderAmount = 1000m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 100, UsedCount = 0 },
                    new Coupon { Code = "BEATVIP", DiscountType = "Percentage", DiscountPercentage = 15m, MinimumOrderAmount = 3000m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 50, UsedCount = 0 },
                    new Coupon { Code = "FREESHIP", DiscountType = "Shipping", DiscountAmount = 0m, MinimumOrderAmount = 0m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 500, UsedCount = 0 }
                };
                await context.Coupons.AddRangeAsync(coupons);
                await context.SaveChangesAsync();
            }

            // Sync inventories for all existing products
            var existingProducts = await context.Products.Include(p => p.Variants).ToListAsync();
            foreach (var p in existingProducts)
            {
                await CreateOrSyncInventory(context, p);
            }
            await context.SaveChangesAsync();

            return;
        }

        var rnd = new Random(12345);

        if (force)
        {
            context.ReturnRequests.RemoveRange(context.ReturnRequests);
            context.Payments.RemoveRange(context.Payments);
            context.OrderItems.RemoveRange(context.OrderItems);
            context.Orders.RemoveRange(context.Orders);
            context.CartItems.RemoveRange(context.CartItems);
            context.Carts.RemoveRange(context.Carts);
            context.WishlistItems.RemoveRange(context.WishlistItems);
            context.Notifications.RemoveRange(context.Notifications);
            context.AuditLogs.RemoveRange(context.AuditLogs);
            context.ProductReviews.RemoveRange(context.ProductReviews);
            context.ProductImages.RemoveRange(context.ProductImages);
            context.ProductFaqs.RemoveRange(context.ProductFaqs);
            context.ProductVariantImages.RemoveRange(context.ProductVariantImages);
            context.ProductVariants.RemoveRange(context.ProductVariants);
            context.Inventories.RemoveRange(context.Inventories);
            context.InventoryHistories.RemoveRange(context.InventoryHistories);
            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);
            await context.SaveChangesAsync();
        }

        // Seed clean categories
        var categoriesMap = new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase);
        foreach (var catName in SeededCategories)
        {
            var cat = new Category
            {
                Id = Guid.NewGuid(),
                Name = catName,
                Description = $"High fidelity {catName} sound systems from premium brands."
            };
            context.Categories.Add(cat);
            categoriesMap[catName] = cat;
        }
        await context.SaveChangesAsync();

        var admin = await EnsureAdminAsync(userManager, roleManager);
        await EnsureRegularUserAsync(userManager, roleManager);

        // Seed new curated catalog
        var savedProducts = new List<Product>();
        foreach (var temp in ProductTemplates)
        {
            if (!categoriesMap.TryGetValue(temp.CategoryName, out var category))
            {
                continue;
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = temp.Name,
                Description = temp.Description,
                CategoryId = category.Id,
                Brand = temp.Brand,
                Rating = Math.Round(4.0 + rnd.NextDouble() * 1.0, 1),
                BatteryLife = temp.BatteryLife,
                Connectivity = temp.Connectivity,
                IsFeatured = rnd.NextDouble() > 0.6,
                SoldCount = rnd.Next(50, 800),
                DeliveryDays = rnd.Next(2, 5),
                Faqs = CreateFaqs(),
                Reviews = CreateReviews(admin?.Id, rnd)
            };

            var variants = new List<ProductVariant>();
            for (int i = 0; i < temp.Variants.Count; i++)
            {
                var vt = temp.Variants[i];
                var images = BuildImagesForVariant(temp.CategoryName, vt.ImageFilter, i, 4);

                variants.Add(new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Color = vt.Color,
                    ColorCode = vt.ColorCode,
                    Price = vt.Price,
                    DiscountPrice = vt.DiscountPrice,
                    StockQuantity = rnd.Next(30, 150),
                    Sku = vt.Sku,
                    IsActive = true,
                    Images = images
                });
            }

            product.Variants = variants;
            product.Images = CreateImagesFromVariants(variants, temp.CategoryName);

            context.Products.Add(product);
            savedProducts.Add(product);
        }
        await context.SaveChangesAsync();

        // Build Inventories
        foreach (var p in savedProducts)
        {
            await CreateOrSyncInventory(context, p);
        }

        if (isDevelopment && !await context.Coupons.AnyAsync())
        {
            var coupons = new List<Coupon>
            {
                new Coupon { Code = "DEAL10", DiscountType = "Percentage", DiscountPercentage = 10m, MinimumOrderAmount = 1000m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 100, UsedCount = 0 },
                new Coupon { Code = "BEATVIP", DiscountType = "Percentage", DiscountPercentage = 15m, MinimumOrderAmount = 3000m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 50, UsedCount = 0 },
                new Coupon { Code = "FREESHIP", DiscountType = "Shipping", DiscountAmount = 0m, MinimumOrderAmount = 0m, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 500, UsedCount = 0 }
            };
            await context.Coupons.AddRangeAsync(coupons);
        }

        await context.SaveChangesAsync();
    }

    private static async Task<AppUser?> EnsureAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminEmail = "vikram.admin@beatbox.com";
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Vikram Singh (Admin)",
                IsEmailVerified = true,
                IsPhoneVerified = true
            };

            var result = await userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(admin, "Admin"))
                await userManager.AddToRoleAsync(admin, "Admin");
        }
        return admin;
    }

    private static async Task EnsureRegularUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var userEmail = "User@beatbox.com";
        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        var standardUser = await userManager.FindByEmailAsync(userEmail);
        if (standardUser == null)
        {
            standardUser = new AppUser
            {
                UserName = userEmail,
                Email = userEmail,
                FullName = "BeatBox User",
                IsEmailVerified = true,
                IsPhoneVerified = true
            };

            var result = await userManager.CreateAsync(standardUser, "User@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(standardUser, "User");
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(standardUser, "User"))
                await userManager.AddToRoleAsync(standardUser, "User");
        }
    }

    private static List<ProductImage> CreateImagesFromVariants(IEnumerable<ProductVariant> variants, string categoryName)
    {
        var productImages = new List<ProductImage>();
        var first = true;

        foreach (var v in variants)
        {
            foreach (var img in v.Images.OrderBy(i => i.DisplayOrder))
            {
                productImages.Add(new ProductImage
                {
                    ImageUrl = img.ImageUrl,
                    ColorName = v.Color,
                    ColorCode = v.ColorCode,
                    IsPrimary = first
                });
                first = false;
            }
        }
        return productImages;
    }

    private static List<ProductFaq> CreateFaqs()
    {
        return new List<ProductFaq>
        {
            new ProductFaq { Question = "What is the warranty period?", Answer = "All BeatBox products come with a 1 year warranty." },
            new ProductFaq { Question = "Can I request returns?", Answer = "Yes, returns are accepted within 7 days in original packaging." }
        };
    }

    private static List<ProductReview> CreateReviews(string? adminUserId, Random rnd)
    {
        var texts = new[]
        {
            "Amazing clarity and punchy bass!",
            "Great battery backup and fit is premium.",
            "Really impressive quality for this price point.",
            "Very fast delivery, product is extremely premium."
        };

        var reviews = new List<ProductReview>();
        for (int i = 0; i < 2; i++)
        {
            reviews.Add(new ProductReview
            {
                UserId = adminUserId ?? string.Empty,
                Rating = rnd.Next(4, 6),
                Comment = texts[rnd.Next(texts.Length)],
                CreatedDate = DateTime.UtcNow.AddDays(-rnd.Next(1, 15)),
                IsVerifiedPurchase = true
            });
        }
        return reviews;
    }

    private static async Task CreateOrSyncInventory(AppDbContext context, Product product)
    {
        var total = product.Variants?.Sum(v => v.StockQuantity) ?? 0;
        var inv = await context.Inventories.FirstOrDefaultAsync(i => i.ProductId == product.Id);
        if (inv == null)
        {
            inv = new Inventory
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                AvailableStock = total,
                ReservedStock = 0,
                WarehouseLocation = "Main Warehouse",
                LowStockThreshold = 5,
                LastUpdated = DateTime.UtcNow
            };
            context.Inventories.Add(inv);

            context.InventoryHistories.Add(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = inv.AvailableStock,
                Reason = "InitialSeed",
                Timestamp = DateTime.UtcNow,
                PerformedBy = "system"
            });
        }
        else
        {
            if (inv.AvailableStock != total)
            {
                var diff = total - inv.AvailableStock;
                inv.AvailableStock = total;
                inv.LastUpdated = DateTime.UtcNow;
                context.InventoryHistories.Add(new InventoryHistory
                {
                    Id = Guid.NewGuid(),
                    InventoryId = inv.Id,
                    Change = diff,
                    Reason = "SyncSeed",
                    Timestamp = DateTime.UtcNow,
                    PerformedBy = "system"
                });
            }
        }
    }
}
