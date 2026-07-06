using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;

namespace Infrastructure.Data;

public static class DbSeeder
{
    // Base path for product images in wwwroot
    private const string ImageBasePath = "/images/products";

    // Placeholder image path for missing images
    private const string PlaceholderImagePath = "/images/products/placeholder.jpg";

    // Centralized category -> image lists (local file paths).
    // Images are loaded from wwwroot/images/products/{category}/ folders.
    private static Dictionary<string, string[]> CategoryImages = new(StringComparer.OrdinalIgnoreCase);

    // Root content path for accessing wwwroot (will be injected via configuration or constructor if needed)
    private static string _contentRootPath = "";

    public static readonly string[] FrontendCategories = new[]
    {
        "Audio",
        "Soundbars",
        "Party Speakers",
        "Portable Speakers",
        "TWS",
        "Neckbands",
        "Wireless Headphones",
        "Wired Earphones",
        "USB Speakers",
        "Conference Speakers",
        "Wireless Microphones",
        "Mobile Accessories",
        "Power Bank",
        "Cables",
        "Wireless Charger",
        "Chargers",
        "Mobile Holder",
        "Gadget Cleaners",
        "Phone Wallet",
        "Cable Organiser",
        "Computer Accessories",
        "Keyboard And Mouse",
        "Wireless Keyboard",
        "Wired Keyboard",
        "Gaming Keyboard",
        "Wireless Mouse",
        "Wired Mouse",
        "Laptop Stand",
        "Laptop Table",
        "Extension Board",
        "Projectors",
        "USB Hub",
        "LCD Writing Pads",
        "Laptop Bags",
        "Computer Cables",
        "Wireless Presenter",
        "Car Accessories",
        "Car Charger",
        "Car Bluetooth",
        "Tyre Inflator",
        "Car Mobile Holder",
        "Bike Mobile Holder",
        "Vacuum Cleaner",
        "Car Wireless Charger",
        "Pressure Washer",
        "Smart Gadgets",
        "Ear Cleaners",
        "Portable Fans",
        "Selfie Stick",
        "Flashlight",
        "Stylus",
        "Location Tracker",
        "Electric Kettle",
        "Hair Dryer",
        "Tool Kit",
        "Humidifiers",
        "Air Blower",
        "Timers",
        "Massagers",
        "Smart Sealers",
        "Rechargeable Battery"
    };

    private static readonly Dictionary<string, string> CategoryToFolderMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Explicit/custom mappings
        { "Keyboard And Mouse", "keyboards" },
        { "Gaming Keyboard", "gaming-keyboards" },
        { "Wired Keyboard", "keyboards" },
        { "Wireless Keyboard", "keyboards" },
        { "Wired Mouse", "mice" },
        { "Wireless Mouse", "mice" },
        { "Tyre Inflator", "tyre-inflators" },
        { "Laptop Stand", "laptop-bags" },
        { "Laptop Table", "laptop-bags" },
        { "Car Mobile Holder", "mobile-holder" },
        { "Bike Mobile Holder", "mobile-holder" },
        { "Car Wireless Charger", "wireless-charger" },
        { "Computer Cables", "cables" },
        { "USB Hub", "cables" },
        { "Gadget Cleaners", "ear-cleaners" },
        { "Audio", "soundbars" },
        { "Cable Organiser", "cables" },
        { "Phone Wallet", "mobile-holder" },
        { "Extension Board", "chargers" },
        { "Wireless Presenter", "stylus" },

        // Direct/Derived mappings (mapped explicitly to guarantee resolution)
        { "Soundbars", "soundbars" },
        { "Party Speakers", "party-speakers" },
        { "Portable Speakers", "bluetooth_speaker" },
        { "TWS", "tws_eaebuds" },
        { "Neckbands", "neckbands" },
        { "Wireless Headphones", "wireless-headphones" },
        { "Wired Earphones", "wired_headphones" },
        { "USB Speakers", "usb-speakers" },
        { "Conference Speakers", "conference-speakers" },
        { "Wireless Microphones", "wireless-microphones" },
        { "Power Bank", "power-bank" },
        { "Cables", "cables" },
        { "Wireless Charger", "wireless-charger" },
        { "Chargers", "chargers" },
        { "Mobile Holder", "mobile-holder" },
        { "Keyboards", "keyboards" },
        { "Mice", "mice" },
        { "Gaming Keyboards", "gaming-keyboards" },
        { "Laptop Bags", "laptop-bags" },
        { "Projectors", "projectors" },
        { "Car Charger", "car-charger" },
        { "Car Bluetooth", "car-bluetooth" },
        { "Tyre Inflators", "tyre-inflators" },
        { "Ear Cleaners", "ear-cleaners" },
        { "Portable Fans", "portable-fans" },
        { "Selfie Stick", "selfie-stick" },
        { "Flashlight", "flashlight" },
        { "Stylus", "stylus" },
        { "Electric Kettle", "electric-kettle" },
        { "Hair Dryer", "hair-dryer" },
        { "Humidifiers", "humidifiers" },
        { "Massagers", "massagers" },
        { "Rechargeable Battery", "rechargeable-battery" }
    };

    /// <summary>
    /// Sets the content root path for image file validation.
    /// Call this early in the application startup.
    /// </summary>
    public static void SetContentRootPath(string contentRootPath)
    {
        _contentRootPath = contentRootPath;
    }

    private static string? ResolveFolderForCategory(string categoryName)
    {
        // 1. Direct explicit mapping
        if (CategoryToFolderMap.TryGetValue(categoryName, out var folder))
        {
            var explicitFolderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", folder);
            if (Directory.Exists(explicitFolderPath))
            {
                return folder;
            }
        }

        // 2. Derive folder name: convert spaces to dashes, lowercase
        var derived = categoryName.Replace(" ", "-").ToLowerInvariant();
        var folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", derived);
        if (Directory.Exists(folderPath))
        {
            return derived;
        }

        // 3. No mapping available -> returns null (caller will handle fallback to default)
        return null;
    }

    /// <summary>
    /// Initializes the image pools by loading category images from the filesystem.
    /// Call this after SetContentRootPath().
    /// </summary>
    public static void InitializeImagePools()
    {
        if (string.IsNullOrWhiteSpace(_contentRootPath))
        {
            throw new InvalidOperationException("Content root path is not set.");
        }

        Console.WriteLine("\nInitializing Image Pools...");
        Console.WriteLine(string.Format("{0,-25} | {1,-25} | {2,-11}", "Category", "Folder", "Image Count"));
        Console.WriteLine(new string('-', 67));

        var map = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        var categoriesUsingDefault = new List<string>();

        // Load the default image pool once
        var defaultImages = LoadCategoryImages("default");
        map["default"] = defaultImages;

        foreach (var categoryName in FrontendCategories)
        {
            var folder = ResolveFolderForCategory(categoryName);
            string[] images;
            string displayFolder;

            if (folder != null)
            {
                displayFolder = folder;
                images = LoadCategoryImages(folder);
            }
            else
            {
                displayFolder = "default";
                images = defaultImages;
                categoriesUsingDefault.Add(categoryName);
            }

            map[categoryName] = images;

            // Log: Category | Folder | Image Count
            Console.WriteLine(string.Format("{0,-25} | {1,-25} | {2,-11}", categoryName, displayFolder, images.Length));
        }

        CategoryImages = map;

        // Print all categories still using default after initialization
        if (categoriesUsingDefault.Any())
        {
            Console.WriteLine("\nCategories falling back to 'default':");
            foreach (var cat in categoriesUsingDefault)
            {
                Console.WriteLine($"- {cat}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Loads all image filenames from a category folder in wwwroot/images/products/.
    /// Returns relative paths suitable for HTML img src attributes.
    /// </summary>
    private static string[] LoadCategoryImages(string categoryFolder)
    {
        if (string.IsNullOrWhiteSpace(_contentRootPath))
        {
            throw new InvalidOperationException("Content root path is not set.");
        }

        var folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", categoryFolder);

        // If folder doesn't exist, throw an exception
        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Category image folder not found: {folderPath}");
        }

        // Get all image files (jpg, jpeg, png, webp, gif)
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
            .OrderBy(f => f)
            .ToList();

        // If no images found, throw an exception
        if (!imageFiles.Any())
        {
            throw new FileNotFoundException($"No images found in category folder: {folderPath}");
        }

        // Convert file paths to relative web paths (/images/products/category/filename)
        var imagePaths = imageFiles
            .Select(f =>
            {
                var fileName = Path.GetFileName(f);
                return $"{ImageBasePath}/{categoryFolder}/{fileName}";
            })
            .ToArray();

        // Ensure we have at least 3 images by circular padding using the existing images
        if (imagePaths.Length < 3)
        {
            var paddedList = imagePaths.ToList();
            while (paddedList.Count < 3)
            {
                paddedList.Add(imagePaths[paddedList.Count % imagePaths.Length]);
            }
            return paddedList.ToArray();
        }

        return imagePaths;
    }

    // Build N ProductVariantImage objects for a category using the CategoryImages pool.
    // Tracks globally used URLs to prevent any image reuse across variants of the same product.
    private static List<ProductVariantImage> BuildVariantImagesForCategory(
        string categoryName, 
        int count, 
        Random rnd, 
        HashSet<string> globallyUsedUrls = null)
    {
        globallyUsedUrls ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var pool = CategoryImages.TryGetValue(categoryName, out var catPool) 
            ? catPool 
            : (CategoryImages.TryGetValue("default", out var d) ? d : CategoryImages.Values.First());

        var list = new List<ProductVariantImage>();
        var attemptsPerImage = 0;
        var maxAttemptsPerImage = pool.Length * 2;

        for (int i = 0; i < count; i++)
        {
            string selectedUrl = null;
            attemptsPerImage = 0;

            // Find an image URL not yet used in this global context
            while (attemptsPerImage < maxAttemptsPerImage)
            {
                var idx = rnd.Next(0, pool.Length);
                var candidateUrl = pool[idx];

                if (!globallyUsedUrls.Contains(candidateUrl))
                {
                    selectedUrl = candidateUrl;
                    globallyUsedUrls.Add(candidateUrl);
                    break;
                }

                attemptsPerImage++;
            }

            // Fallback if pool is exhausted: cycle through remaining unused images
            if (selectedUrl == null)
            {
                foreach (var url in pool)
                {
                    if (!globallyUsedUrls.Contains(url))
                    {
                        selectedUrl = url;
                        globallyUsedUrls.Add(url);
                        break;
                    }
                }
            }

            // Last resort: if entire pool is used, wrap around (should be rare)
            if (selectedUrl == null && pool.Length > 0)
            {
                selectedUrl = pool[i % pool.Length];
                globallyUsedUrls.Add(selectedUrl);
            }

            // Validate image exists or use placeholder
            selectedUrl = ValidateAndGetImagePath(selectedUrl);

            list.Add(new ProductVariantImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = selectedUrl,
                IsPrimary = i == 0,
                DisplayOrder = i + 1
            });
        }

        return list;
    }

    /// <summary>
    /// Validates that an image file exists at the given path.
    /// Throws an exception if it doesn't exist.
    /// </summary>
    private static string ValidateAndGetImagePath(string imagePath)
    {
        // If no content root path is set, trust the path as-is (development/testing scenario)
        if (string.IsNullOrWhiteSpace(_contentRootPath))
        {
            return imagePath;
        }

        // Construct full file path from the relative web path
        // imagePath: /images/products/category/filename.jpg
        // fullPath: {contentRoot}/wwwroot/images/products/category/filename.jpg
        var relativePath = imagePath.TrimStart('/');
        var fullPath = Path.Combine(_contentRootPath, "wwwroot", relativePath);

        // Check if file exists
        if (File.Exists(fullPath))
        {
            return imagePath;
        }

        // File doesn't exist, throw exception
        throw new FileNotFoundException($"Seed image file not found on disk: {fullPath}");
    }

    // Seed entry point
    public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, bool force = false, bool isDevelopment = false)
    {

        if (context == null) throw new ArgumentNullException(nameof(context));
        if (userManager == null) throw new ArgumentNullException(nameof(userManager));
        if (roleManager == null) throw new ArgumentNullException(nameof(roleManager));

        if (!force && (await context.Categories.AnyAsync() || await context.Products.AnyAsync()))
        {
            await EnsureAdminAsync(userManager, roleManager);
            if (isDevelopment && !await context.Coupons.AnyAsync())
            {
                var coupons = new List<Coupon>
                {
                    new Coupon { Code = "DEAL10", DiscountPercentage = 10, MinimumOrderAmount = 1000, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 100, UsedCount = 0 },
                    new Coupon { Code = "BEATVIP", DiscountPercentage = 15, MinimumOrderAmount = 3000, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 50, UsedCount = 0 },
                    new Coupon { Code = "FREESHIP", DiscountAmount = 0, MinimumOrderAmount = 0, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 500, UsedCount = 0 }
                };
                await context.Coupons.AddRangeAsync(coupons);
                await context.SaveChangesAsync();
            }
            return;
        }

        // Keep deterministic random data
        var rnd = new Random(12345);


        await SeedCategories(context);

        // Ensure admin user exists for reviews
        var admin = await EnsureAdminAsync(userManager, roleManager);

        // Ensure standard user exists
        await EnsureRegularUserAsync(userManager, roleManager);

        await SeedProductsAsync(context, admin?.Id, rnd);

        // Ensure all products (including existing ones from previous seedings/runs) have variants
        await EnsureAllProductsHaveVariantsAsync(context, rnd);

        // Seed coupons in development
        if (isDevelopment && !await context.Coupons.AnyAsync())
        {
            var coupons = new List<Coupon>
            {
                new Coupon { Code = "DEAL10", DiscountPercentage = 10, MinimumOrderAmount = 1000, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 100, UsedCount = 0 },
                new Coupon { Code = "BEATVIP", DiscountPercentage = 15, MinimumOrderAmount = 3000, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 50, UsedCount = 0 },
                new Coupon { Code = "FREESHIP", DiscountAmount = 0, MinimumOrderAmount = 0, ExpiryDate = DateTime.UtcNow.AddDays(30), IsActive = true, UsageLimit = 500, UsedCount = 0 }
            };
            await context.Coupons.AddRangeAsync(coupons);
        }

        // Final save
        await context.SaveChangesAsync();
    }

    private static async Task EnsureAllProductsHaveVariantsAsync(AppDbContext context, Random rnd)
    {
        var productsWithoutVariants = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Images)
            .Where(p => !p.Variants.Any())
            .ToListAsync();

        if (productsWithoutVariants.Any())
        {
            Console.WriteLine($"Found {productsWithoutVariants.Count} products without variants. Seeding variants...");
            foreach (var product in productsWithoutVariants)
            {
                try
                {
                    var categoryName = product.Category?.Name ?? "default";
                    var variantCount = rnd.Next(3, 6);
                    var variants = CreateVariants(variantCount, rnd, categoryName);
                    
                    foreach (var variant in variants)
                    {
                        variant.ProductId = product.Id;
                        product.Variants.Add(variant);
                        context.Entry(variant).State = EntityState.Added;
                        foreach (var img in variant.Images)
                        {
                            context.Entry(img).State = EntityState.Added;
                        }
                    }

                    // Also ensure we sync inventories for these products
                    await CreateOrSyncInventory(context, product);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"Concurrency error on product {product.Name} (Id: {product.Id}): {ex.Message}");
                    foreach (var entry in ex.Entries)
                    {
                        Console.WriteLine($"Entity Type: {entry.Entity.GetType().FullName}, State: {entry.State}");
                        var databaseValues = await entry.GetDatabaseValuesAsync();
                        if (databaseValues == null)
                        {
                            Console.WriteLine("The entity was deleted by another user or cannot be found.");
                        }
                        else
                        {
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                    try
                    {
                        await context.SaveChangesAsync();
                        Console.WriteLine("Successfully resolved concurrency error.");
                    }
                    catch (Exception retryEx)
                    {
                        Console.WriteLine($"Failed to resolve concurrency error: {retryEx.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding variants for {product.Name} (Id: {product.Id}): {ex.Message}");
                }
            }
            Console.WriteLine($"Finished variant check for {productsWithoutVariants.Count} products.");
        }
    }

    // Ensure all frontend categories exist
    private static async Task SeedCategories(AppDbContext context)
    {
        foreach (var name in FrontendCategories)
        {
            var exists = await context.Categories.AnyAsync(c => c.Name == name);
            if (!exists)
            {
                context.Categories.Add(new Category
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = name
                });
            }
        }


        await context.SaveChangesAsync();
    }

    private static async Task<AppUser?> EnsureAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminEmail = "BeatBox@admin.com";
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "BeatBox Admin",
                IsEmailVerified = true,
                IsPhoneVerified = true
            };

            var result = await userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            else
            {
                // If user creation failed, return null to avoid FK violations when adding reviews
                return null;
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
            else
            {
                Console.WriteLine("Failed to create standard user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(standardUser, "User"))
                await userManager.AddToRoleAsync(standardUser, "User");
        }
    }

    // Seed products for every category ensuring at least 5 products per category and 250+ total
    private static async Task SeedProductsAsync(AppDbContext context, string? adminUserId, Random rnd)
    {
        var categories = await context.Categories.ToListAsync();

        // Product name parts and brands
        var brands = new[] { "BeatBox", "SoundCore", "PulseTech", "NeoAudio", "Waveform", "AudioMax", "ClearTone", "BassLine", "ProSound", "EchoLabs" };
        var adjectives = new[] { "Pro", "Max", "Lite", "Mini", "Ultra", "Plus", "Go", "Prime", "Elite", "Neo" };
        var nouns = new[] { "Speaker", "Headset", "Earbuds", "Charger", "Stand", "Tracker", "Cleaner", "Hub", "Kettle", "Blower" };

        var targetPerCategory = 5; // minimum

        var globalProducts = new List<Product>();

        foreach (var cat in categories)
        {
            // ensure at least targetPerCategory products in each category
            var existingCount = await context.Products.CountAsync(p => p.CategoryId == cat.Id);
            var toCreate = Math.Max(0, targetPerCategory - existingCount);

            for (int i = 0; i < toCreate; i++)
            {
                var brand = brands[rnd.Next(brands.Length)];
                var name = CreateProductName(cat.Name, adjectives, nouns, rnd);
                var product = CreateProduct(name, cat.Id, brand, rnd);

                // Variants (3-5)
                var variantCount = rnd.Next(3, 6);
                product.Variants = CreateVariants(variantCount, rnd, cat.Name);

                // Reviews
                product.Reviews = CreateReviews(adminUserId, rnd);

                // FAQs
                product.Faqs = CreateFaqs();

                // --- VALIDATIONS (strict) ---
                // Validate variants and their images
                foreach (var variant in product.Variants)
                {
                    if (variant.Price <= 0)
                        throw new Exception($"Invalid Price for variant of {product.Name}");
                    if (variant.DiscountPrice <= 0)
                        throw new Exception($"Invalid DiscountPrice for variant of {product.Name}");
                    if (variant.DiscountPrice >= variant.Price)
                        throw new Exception($"DiscountPrice must be lower than Price for variant of {product.Name}");
                    // Ensure variant images exist and are valid
                    if (variant.Images == null || variant.Images.Count < 3)
                        throw new Exception($"Variant for {product.Name} must have at least 3 images.");
                    if (!variant.Images.Any(img => img.IsPrimary))
                        throw new Exception($"Variant for {product.Name} must have an IsPrimary image.");
                    var orders = variant.Images.Select(img => img.DisplayOrder).ToList();
                    if (orders.Min() != 1)
                        throw new Exception($"Variant images for {product.Name} must have DisplayOrder starting at 1.");
                    if (orders.Distinct().Count() != orders.Count)
                        throw new Exception($"Variant images for {product.Name} contain duplicate DisplayOrder values.");
                }
                // No product-level color images are seeded from variants anymore

                globalProducts.Add(product);
            }
        }

            if (globalProducts.Any())
            {
                // Add products with populated variant images so EF can persist ProductVariantImages
                await context.Products.AddRangeAsync(globalProducts);
                await context.SaveChangesAsync();

                // After saving products, ensure variant images were persisted (they should be via FK)
                // Create inventories and inventory histories
                var savedProducts = await context.Products.Include(p => p.Variants).ThenInclude(v => v.Images).ToListAsync();
                foreach (var p in savedProducts)
                {
                    await CreateOrSyncInventory(context, p);
                }

                await context.SaveChangesAsync();

                // --- Ensure existing products/variants in DB have variant images ---
                // Load all products with variants and variant images
                var allProducts = await context.Products
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
                    .ToListAsync();

                foreach (var prod in allProducts)
                {
                    var catName = (await context.Categories.Where(c => c.Id == prod.CategoryId).Select(c => c.Name).FirstOrDefaultAsync()) ?? "default";
                    foreach (var variant in prod.Variants)
                    {
                        var currentCount = variant.Images?.Count ?? 0;
                        if (currentCount >= 3) continue; // already populated

                        var need = 3 - currentCount;
                        if (need <= 0) need = 3; // safe fallback

                        var imgs = BuildVariantImagesForCategory(catName, need, rnd);
                        variant.Images ??= new List<ProductVariantImage>();
                        int nextOrder = (variant.Images.Any() ? variant.Images.Max(x => x.DisplayOrder) : 0) + 1;
                        foreach (var vi in imgs)
                        {
                            vi.DisplayOrder = nextOrder++;
                            vi.VariantId = variant.Id;
                            variant.Images.Add(vi);
                        }
                    }
                }

                // Persist newly added variant images
                await context.SaveChangesAsync();
            }
    }

    private static string CreateProductName(string categoryName, string[] adjectives, string[] nouns, Random rnd)
    {
        // Build readable product names based on category context
        var adj = adjectives[rnd.Next(adjectives.Length)];
        var noun = nouns[rnd.Next(nouns.Length)];
        var suffix = rnd.Next(100, 999);

        // Use category keyword if meaningful
        var catToken = categoryName.Split(' ').First();
        var name = new StringBuilder();
        name.Append(catToken);
        name.Append(' ');
        name.Append(adj);
        name.Append(' ');
        name.Append(noun);
        name.Append(' ');
        name.Append(suffix);

        return name.ToString();
    }

    private static Product CreateProduct(string name, Guid categoryId, string brand, Random rnd)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = $"{name} by {brand} — reliable, high-quality product designed for everyday use.",
            CategoryId = categoryId,
            Brand = brand,
            Rating = Math.Round(3.5 + rnd.NextDouble() * 1.5, 1),
            BatteryLife = GenerateBatteryLife(rnd),
            Connectivity = GenerateConnectivity(rnd),
            IsFeatured = rnd.NextDouble() > 0.8,
            SoldCount = rnd.Next(10, 1000),
            DeliveryDays = rnd.Next(1, 7),
            Variants = new List<ProductVariant>(),
            Images = new List<ProductImage>(),
            Faqs = new List<ProductFaq>(),
            Reviews = new List<ProductReview>()
        };
    }

    private static string GenerateBatteryLife(Random rnd)
    {
        var options = new[] { "N/A", "6 Hours", "8 Hours", "10 Hours", "12 Hours", "24 Hours", "3 Days", "7 Days", "10 Days", "14 Days" };
        return options[rnd.Next(options.Length)];
    }

    private static string GenerateConnectivity(Random rnd)
    {
        var options = new[] { "Bluetooth 5.3", "Bluetooth 5.2", "Bluetooth 5.0", "USB", "USB-C", "Wireless", "3.5mm", "WiFi" };
        return options[rnd.Next(options.Length)];
    }

    private static List<ProductVariant> CreateVariants(int count, Random rnd, string categoryName)
    {
        var colors = new[]
        {
            ("Black","#111111"), ("White","#FFFFFF"), ("Blue","#2563EB"), ("Red","#DC2626"), ("Green","#10B981"),
            ("Grey","#6B7280"), ("Gold","#D4AF37"), ("Silver","#C0C0C0"), ("Purple","#7C3AED"), ("Orange","#EA580C")
        };

        // Price bands based on category roughness
        var basePrice = GetBasePriceForCategory(categoryName);

        var variants = new List<ProductVariant>();
        // Global tracker to ensure NO image URL is reused across ANY variant of this product
        var globallyUsedUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < count; i++)
        {
            var c = colors[i % colors.Length];
            // Price logic: ensure an integer price between sensible ranges
            var price = basePrice + (i * 500) + (decimal)(rnd.Next(0, 1000));
            if (price < 1) price = Math.Abs(price) + 499m;

            // Discount percent 5..40 inclusive
            var discountPercent = rnd.Next(5, 41);
            var discountPrice = Math.Round(price * (100m - discountPercent) / 100m, 2);

            // Enforce mandatory rules
            if (discountPrice <= 0m)
            {
                discountPercent = 5;
                discountPrice = Math.Round(price * 0.95m, 2);
            }

            if (discountPrice >= price)
            {
                // ensure always strictly less than price
                discountPrice = price - 1m;
            }

            // Final clamps
            discountPrice = Math.Max(discountPrice, 1m);
            if (discountPrice >= price) discountPrice = price - 1m;
            var stock = rnd.Next(10, 200);

            // pick unique images for this variant from the category pool (3-5 images)
            // Pass globallyUsedUrls to ensure no reuse across variants
            var imagesCount = rnd.Next(3, 6); // 3..5 images per variant
            var imagesForVariant = BuildVariantImagesForCategory(categoryName, imagesCount, rnd, globallyUsedUrls);

            variants.Add(new ProductVariant
            {
                Id = Guid.NewGuid(),
                Color = c.Item1,
                ColorCode = c.Item2,
                Price = Math.Round(price, 0),
                DiscountPrice = discountPrice,
                StockQuantity = stock,
                Images = imagesForVariant
            });
        }

        return variants;
    }

    private static decimal GetBasePriceForCategory(string category)
    {
        // crude price banding based on category keywords
        var low = new[] { "Accessory", "Cable", "Holder", "Cleaner", "Organiser", "Stylus", "Phone Wallet" };
        var mid = new[] { "TWS", "Neckbands", "Portable", "USB", "Mobile", "Computer", "Wireless" };
        var high = new[] { "Projectors", "Pressure", "Smart", "Kettle", "Blower", "Massagers" };

        var name = category.ToLowerInvariant();
        if (high.Any(h => name.Contains(h.ToLowerInvariant()))) return 8999m;
        if (name.Contains("watch")) return 5999m;
        if (name.Contains("speaker") || name.Contains("headphone") || name.Contains("earbuds") || name.Contains("tws")) return 3999m;
        if (mid.Any(m => name.Contains(m.ToLowerInvariant()))) return 2999m;
        return 1999m;
    }

    private static string GetPlaceholderImageUrl(string categoryName, int variantIndex)
    {
        // Use unsplash random images with a query derived from category
        var query = Uri.EscapeDataString(categoryName.Split(' ').First());
        return $"https://source.unsplash.com/collection/190727/800x600?{query}&v={variantIndex}";
    }

    private static List<ProductReview> CreateReviews(string? adminUserId, Random rnd)
    {
        var texts = new[]
        {
            "Excellent quality.",
            "Battery life is amazing.",
            "Worth the price.",
            "Fast delivery.",
            "Premium build quality.",
            "Comfortable and light.",
            "Soundstage is impressive.",
            "Good value for money.",
            "Setup was easy and intuitive.",
            "Noise cancellation works well."
        };

        var reviews = new List<ProductReview>();
        for (int i = 0; i < 3; i++)
        {
            var text = texts[rnd.Next(texts.Length)];
            reviews.Add(new ProductReview
            {
                ProductId = Guid.Empty, // filled by EF when attached to product
                UserId = adminUserId ?? string.Empty,
                Rating = rnd.Next(3, 6),
                Comment = text,
                CreatedDate = DateTime.UtcNow.AddDays(-rnd.Next(1, 30)),
                IsVerifiedPurchase = true
            });
        }

        return reviews;
    }

    private static List<ProductFaq> CreateFaqs()
    {
        return new List<ProductFaq>
        {
            new ProductFaq { Question = "What is the warranty period?", Answer = "All BeatBox products come with a 1 year warranty." },
            new ProductFaq { Question = "How long does it take to charge?", Answer = "Charging time depends on the variant; typically 1-3 hours." },
            new ProductFaq { Question = "Can I return this product?", Answer = "Yes, returns are accepted within 7 days if in original condition." }
        };
    }

    private static List<ProductImage> CreateImagesFromVariants(IEnumerable<ProductVariant> variants, string categoryName, string productName)
    {
        var images = new List<ProductImage>();
        var variantList = variants.ToList();
        if (!variantList.Any()) return images;

        // Need between 4 and 5 images, prefer 5
        var desired = Math.Max(4, Math.Min(5, Math.Max(4, variantList.Count)));

        // Use category image pool if available
        var usedUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var pool = CategoryImages.TryGetValue(categoryName, out var catPool) ? catPool : CategoryImages.TryGetValue("default", out var d) ? d : CategoryImages.Values.First();
        var firstFlag = true;

        // Add one image per variant first (ensure each variant has its own image)
        foreach (var v in variantList)
        {
            if (v.Images != null && v.Images.Any())
            {
                foreach (var vi in v.Images.OrderBy(img => img.DisplayOrder))
                {
                    if (usedUrls.Contains(vi.ImageUrl)) continue;
                    images.Add(new ProductImage
                    {
                        ProductId = Guid.Empty,
                        ImageUrl = vi.ImageUrl,
                        ColorName = v.Color,
                        ColorCode = v.ColorCode,
                        IsPrimary = firstFlag
                    });
                    usedUrls.Add(vi.ImageUrl);
                    firstFlag = false;
                    if (images.Count >= desired) break;
                }
            }
            if (images.Count >= desired) break;
        }

        // If we still need more images, pull unique images from the category pool
        var poolIdx = 0;
        while (images.Count < desired && poolIdx < pool.Length)
        {
            var url = pool[poolIdx++];
            if (string.IsNullOrWhiteSpace(url)) continue;
            if (usedUrls.Contains(url)) continue;
            images.Add(new ProductImage
            {
                ProductId = Guid.Empty,
                ImageUrl = url,
                ColorName = string.Empty,
                ColorCode = string.Empty,
                IsPrimary = false
            });
            usedUrls.Add(url);
        }

        // Ensure one primary is set
        if (!images.Any(i => i.IsPrimary)) images[0].IsPrimary = true;

        // Validation: all images must have non-empty, valid urls
        foreach (var img in images)
        {
            if (string.IsNullOrWhiteSpace(img.ImageUrl))
                throw new Exception($"Missing image for {productName}");
        }

        return images;
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
