using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class MockProductsSeeder
    {
        private const string ImageBasePath = "/images/products";
        private const string PlaceholderImagePath = "/images/products/placeholder.jpg";
        private static Dictionary<string, string[]> CategoryImages = new(StringComparer.OrdinalIgnoreCase);
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
            { "Soundbars", "soundbars" },
            { "Party Speakers", "party-speakers" },
            { "Portable Speakers", "portable-speakers" },
            { "TWS", "tws" },
            { "Neckbands", "neckbands" },
            { "Wireless Headphones", "wireless-headphones" },
            { "Wired Earphones", "wired-earphones" },
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

        public static void SetContentRootPath(string contentRootPath)
        {
            _contentRootPath = contentRootPath;
        }

        private static string? ResolveFolderForCategory(string categoryName)
        {
            if (CategoryToFolderMap.TryGetValue(categoryName, out var folder))
            {
                return folder;
            }

            var derived = categoryName.Replace(" ", "-").ToLowerInvariant();
            var folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", derived);
            if (Directory.Exists(folderPath))
            {
                return derived;
            }

            return null;
        }

        public static void InitializeImagePools()
        {
            if (string.IsNullOrWhiteSpace(_contentRootPath))
            {
                throw new InvalidOperationException("Content root path is not set.");
            }

            var map = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            var defaultImages = LoadCategoryImages("default");
            map["default"] = defaultImages;

            foreach (var categoryName in FrontendCategories)
            {
                var folder = ResolveFolderForCategory(categoryName);
                string[] images;

                if (folder != null)
                {
                    images = LoadCategoryImages(folder);
                }
                else
                {
                    images = defaultImages;
                }

                map[categoryName] = images;
            }

            CategoryImages = map;
        }

        private static string[] LoadCategoryImages(string categoryFolder)
        {
            if (string.IsNullOrWhiteSpace(_contentRootPath))
            {
                throw new InvalidOperationException("Content root path is not set.");
            }

            var folderPath = Path.Combine(_contentRootPath, "wwwroot", "images", "products", categoryFolder);
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"Category image folder not found: {folderPath}");
            }

            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var imageFiles = Directory.GetFiles(folderPath)
                .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                .OrderBy(f => f)
                .ToList();

            if (!imageFiles.Any())
            {
                throw new FileNotFoundException($"No images found in category folder: {folderPath}");
            }

            var imagePaths = imageFiles
                .Select(f =>
                {
                    var fileName = Path.GetFileName(f);
                    return $"{ImageBasePath}/{categoryFolder}/{fileName}";
                })
                .ToArray();

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

                if (selectedUrl == null && pool.Length > 0)
                {
                    selectedUrl = pool[i % pool.Length];
                    globallyUsedUrls.Add(selectedUrl);
                }

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

        private static string ValidateAndGetImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(_contentRootPath))
            {
                return imagePath;
            }

            var relativePath = imagePath.TrimStart('/');
            var fullPath = Path.Combine(_contentRootPath, "wwwroot", relativePath);

            if (File.Exists(fullPath))
            {
                return imagePath;
            }

            throw new FileNotFoundException($"Seed image file not found on disk: {fullPath}");
        }

        private class MockProductData
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
            public string Brand { get; set; } = string.Empty;
            public double Rating { get; set; }
            public string BatteryLife { get; set; } = string.Empty;
            public string Connectivity { get; set; } = string.Empty;
            public bool IsFeatured { get; set; }
            public int SoldCount { get; set; }
            public int DeliveryDays { get; set; }
        }

        private static readonly MockProductData[] MockProducts = new[]
        {
            new MockProductData
            {
                Name = "Rockerz Pro ANC 550",
                Description = "Experience true audio purity with 40mm dynamic drivers, hybrid ANC, and up to 60 hours of massive playback.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.9,
                BatteryLife = "60 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 6040,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Rockerz Wireless 450",
                Description = "Premium wireless headphones with 50-hour battery, signature bass tuning, and ultra-comfortable over-ear cushions.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "50 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 4320,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Airdopes Cyber 141",
                Description = "BEAST™ mode for 40ms gaming latency, quad ENx™ mics for crystal-clear calls, and a glowing neon charging case.",
                CategoryName = "TWS",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "42 Hours Total",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 4780,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Smart Capsule",
                Description = "World's first OLED touchscreen charging case. Adjust EQ, monitor battery, toggle ANC with a swipe on the case.",
                CategoryName = "TWS",
                Brand = "BeatBox",
                Rating = 4.9,
                BatteryLife = "38 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 2060,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Stone Beat Beast 1200",
                Description = "IPX7 waterproof speaker with dual passive radiators, 14W signature bass, custom RGB ring, and waterproof protection.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "14 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 2710,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Stone Grenade Pro",
                Description = "Compact cylindrical speaker with 360° omnidirectional sound. Perfect for travel, indoor parties, and outdoor picnics.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "10 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 1890,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Trip Athletic Neon",
                Description = "Featherlight neckband with magnetic earbud tips, dual EQ modes, and 30 hours of athletic playback for gym warriors.",
                CategoryName = "Neckbands",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "30 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 3615,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Collar Flex Pro",
                Description = "Budget neckband with legendary ASAP Charge — 10 min charging = 10 hours playback. Perfect daily commute companion.",
                CategoryName = "Neckbands",
                Brand = "BeatBox",
                Rating = 4.5,
                BatteryLife = "24 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 2670,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Immortal Cyber Pro",
                Description = "Dedicated per-zone RGB, 50mm drivers, professional boom mic with flip-to-mute, and virtual 7.1 surround for esports pros.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.9,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 4110,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Immortal Rave 700",
                Description = "Seamlessly switch between wireless 2.4GHz (5ms) and wired 3.5mm modes. 40-hour battery. Detachable boom mic.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "40 Hours",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 3205,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Rockerz Club 330",
                Description = "The fan favourite entry-level neckband with 24-hour battery and IPX5 protection. Perfect for first-time buyers.",
                CategoryName = "Neckbands",
                Brand = "BeatBox",
                Rating = 4.4,
                BatteryLife = "24 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 5520,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Storm 111",
                Description = "Premium ANC earbuds with 35-hour battery and CVC 8.0 dual-mic for crystal-clear calls in any environment.",
                CategoryName = "TWS",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "35 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 3435,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Energy Core 10k",
                Description = "Ultra-slim 10000mAh power bank with 22.5W fast charging, LED display, and premium aluminum casing. Never run out of juice.",
                CategoryName = "Power Bank",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 2600,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Blade Pro Trimmer",
                Description = "Precision beard trimmer with self-sharpening titanium blades, 90 mins runtime, and 20 length settings. Elevate your grooming.",
                CategoryName = "Smart Gadgets",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "90 Mins",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 1575,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Cinema Pro Soundbars",
                Description = "Transform your living room into a cinema with 120W of pure Dolby audio and a wireless subwoofer.",
                CategoryName = "Soundbars",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "N/A",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 750,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Premium Braided Cables",
                Description = "Ultra-durable nylon braided cables supporting 60W Power Delivery fast charging.",
                CategoryName = "Cables",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 4450,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Magnetic Wireless Charger",
                Description = "Sleek metallic wireless charging pad with strong magnetic alignment for instantaneous 15W charging.",
                CategoryName = "Wireless Charger",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 2100,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Auto Grip Car Charger",
                Description = "Premium dual-port car charger to fast-charge two devices simultaneously while on the move.",
                CategoryName = "Car Charger",
                Brand = "BeatBox",
                Rating = 4.5,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 1550,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Flexi Mobile Holder",
                Description = "Ergonomic aluminum mobile holder for your desk. Perfect for video calls and content consumption.",
                CategoryName = "Mobile Holder",
                Brand = "BeatBox",
                Rating = 4.4,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 2500,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Ergo Laptop Stand",
                Description = "Premium adjustable aluminum laptop stand to improve your posture and device cooling.",
                CategoryName = "Laptop Stand",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 1100,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Multi-Port USB Hub",
                Description = "Expand your laptop connectivity with this sleek 7-in-1 Type-C hub adapter.",
                CategoryName = "USB Hub",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 900,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Pro Laptop Bags",
                Description = "Professional water-resistant laptop backpack with anti-theft compartments and ergonomic padding.",
                CategoryName = "Laptop Bags",
                Brand = "BeatBox",
                Rating = 4.9,
                BatteryLife = "N/A",
                Connectivity = "Wired",
                IsFeatured = true,
                SoldCount = 725,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Stone 1500",
                Description = "Heavy duty rugged portable bluetooth speaker with 40W sound output and IPX6 water resistance.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "15 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 1200,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Stone 1200",
                Description = "Premium sound experience with 14W RMS sound, RGB lights, and rugged design.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "14 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 3100,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "Stone 650R",
                Description = "Stylish design combined with powerful 10W sound output, Bluetooth v5.0 and IPX5 resistance.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.5,
                BatteryLife = "7 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 850,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Stone 193",
                Description = "Compact ultra-portable speaker with 5W signature sound, lightweight build, and loop strap.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.3,
                BatteryLife = "6 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 450,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Stone Vibe",
                Description = "Immersive sound on the go with this compact wireless speaker featuring custom fabric finish.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.4,
                BatteryLife = "8 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 620,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Stone 350 Pro Naruto Edition",
                Description = "Anime edition 10W portable speaker featuring exclusive Konoha prints and signature bass.",
                CategoryName = "Portable Speakers",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "12 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 1500,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 412",
                Description = "Comfortable wireless headphones with 15 hours playback, voice assistant support, and passive isolation.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.2,
                BatteryLife = "15 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 730,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 413",
                Description = "Upgrade to Rockerz 413 featuring deeper bass response, premium leatherette earcups, and 20 hours battery life.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.4,
                BatteryLife = "20 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 980,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 650 Pro",
                Description = "High fidelity sound with massive 40mm drivers, active noise cancellation, and premium matte finish.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.8,
                BatteryLife = "40 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 2100,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 480",
                Description = "Retro-styled wireless headphones combining vintage design with modern bluetooth 5.0 and 30 hours battery.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "30 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 1350,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 512 ANC",
                Description = "Banish ambient noise with active noise cancellation, 40mm drivers, and up to 35 hours of pure musical bliss.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.7,
                BatteryLife = "35 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 1800,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz Plus 550",
                Description = "Upgraded dynamic sound, soft memory foam earcups, and durable headband. Perfect for music enthusiasts.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.6,
                BatteryLife = "25 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 1420,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 430",
                Description = "Lightweight on-ear headphones with balanced sound signature, inline mic, and dual mode compatibility.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.3,
                BatteryLife = "15 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 590,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 421",
                Description = "Sports oriented wireless headphones with sweat resistant earcups and secure-fit headband design.",
                CategoryName = "Wireless Headphones",
                Brand = "BeatBox",
                Rating = 4.4,
                BatteryLife = "18 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 640,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 371",
                Description = "Comfortable ergonomic neckband featuring metallic earbuds, magnetic tips, and fast charging.",
                CategoryName = "Neckbands",
                Brand = "BeatBox",
                Rating = 4.3,
                BatteryLife = "20 Hours",
                Connectivity = "Wireless",
                IsFeatured = false,
                SoldCount = 1100,
                DeliveryDays = 3
            },
            new MockProductData
            {
                Name = "BeatBox Rockerz 370 Pro",
                Description = "Premium neckband with high-performance drivers, crystal clear calls, and up to 25 hours playback.",
                CategoryName = "Neckbands",
                Brand = "BeatBox",
                Rating = 4.5,
                BatteryLife = "25 Hours",
                Connectivity = "Wireless",
                IsFeatured = true,
                SoldCount = 1750,
                DeliveryDays = 3
            }
        };

        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (roleManager == null) throw new ArgumentNullException(nameof(roleManager));

            var rnd = new Random(54321);

            await SeedCategories(context);

            var admin = await EnsureAdminAsync(userManager, roleManager);

            await SeedProductsAsync(context, admin?.Id, rnd);

            await context.SaveChangesAsync();
        }

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
                else
                {
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

        private static async Task SeedProductsAsync(AppDbContext context, string? adminUserId, Random rnd)
        {
            var existingCategories = await context.Categories.ToDictionaryAsync(c => c.Name.ToLower(), c => c);
            var existingProductNames = new HashSet<string>(await context.Products.Select(p => p.Name.ToLower()).ToListAsync());

            var productsToAdd = new List<Product>();

            foreach (var mockData in MockProducts)
            {
                if (existingProductNames.Contains(mockData.Name.ToLower()))
                {
                    continue;
                }

                var categoryKey = mockData.CategoryName.ToLower();
                if (!existingCategories.TryGetValue(categoryKey, out var category))
                {
                    // Fallback to first matched category or create a new one
                    category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = mockData.CategoryName,
                        Description = mockData.CategoryName
                    };
                    context.Categories.Add(category);
                    existingCategories[categoryKey] = category;
                    await context.SaveChangesAsync();
                }

                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = mockData.Name,
                    Description = mockData.Description,
                    CategoryId = category.Id,
                    Brand = mockData.Brand,
                    Rating = mockData.Rating,
                    BatteryLife = mockData.BatteryLife,
                    Connectivity = mockData.Connectivity,
                    IsFeatured = mockData.IsFeatured,
                    SoldCount = mockData.SoldCount,
                    DeliveryDays = mockData.DeliveryDays,
                    Variants = new List<ProductVariant>(),
                    Images = new List<ProductImage>(),
                    Faqs = new List<ProductFaq>(),
                    Reviews = new List<ProductReview>()
                };

                // Create 3-5 variants
                var variantCount = rnd.Next(3, 6);
                product.Variants = CreateVariants(variantCount, rnd, mockData.CategoryName);

                // Add Reviews
                product.Reviews = CreateReviews(adminUserId, rnd);

                // Add FAQs
                product.Faqs = CreateFaqs();

                // Validate variants
                foreach (var variant in product.Variants)
                {
                    if (variant.Price <= 0)
                        throw new Exception($"Invalid Price for variant of {product.Name}");
                    if (variant.DiscountPrice <= 0)
                        throw new Exception($"Invalid DiscountPrice for variant of {product.Name}");
                    if (variant.DiscountPrice >= variant.Price)
                        throw new Exception($"DiscountPrice must be lower than Price for variant of {product.Name}");
                    if (variant.Images == null || variant.Images.Count < 3)
                        throw new Exception($"Variant for {product.Name} must have at least 3 images.");
                    if (!variant.Images.Any(img => img.IsPrimary))
                        throw new Exception($"Variant for {product.Name} must have an IsPrimary image.");
                }

                productsToAdd.Add(product);
            }

            if (productsToAdd.Any())
            {
                await context.Products.AddRangeAsync(productsToAdd);
                await context.SaveChangesAsync();

                var savedProducts = await context.Products
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
                    .Where(p => productsToAdd.Select(pa => pa.Id).Contains(p.Id))
                    .ToListAsync();

                foreach (var p in savedProducts)
                {
                    await CreateOrSyncInventory(context, p);
                }

                await context.SaveChangesAsync();

                // Ensure all seeded products have correct variant images
                foreach (var prod in savedProducts)
                {
                    foreach (var variant in prod.Variants)
                    {
                        var currentCount = variant.Images?.Count ?? 0;
                        if (currentCount >= 3) continue;

                        var need = 3 - currentCount;
                        var imgs = BuildVariantImagesForCategory(prod.Category?.Name ?? "default", need, rnd);
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

                await context.SaveChangesAsync();
            }
        }

        private static List<ProductVariant> CreateVariants(int count, Random rnd, string categoryName)
        {
            var colors = new[]
            {
                ("Black","#111111"), ("White","#FFFFFF"), ("Blue","#2563EB"), ("Red","#DC2626"), ("Green","#10B981"),
                ("Grey","#6B7280"), ("Gold","#D4AF37"), ("Silver","#C0C0C0"), ("Purple","#7C3AED"), ("Orange","#EA580C")
            };

            var basePrice = GetBasePriceForCategory(categoryName);
            var variants = new List<ProductVariant>();
            var globallyUsedUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < count; i++)
            {
                var c = colors[i % colors.Length];
                var price = basePrice + (i * 500) + (decimal)(rnd.Next(0, 1000));
                if (price < 1) price = Math.Abs(price) + 499m;

                var discountPercent = rnd.Next(5, 41);
                var discountPrice = Math.Round(price * (100m - discountPercent) / 100m, 2);

                if (discountPrice <= 0m)
                {
                    discountPrice = Math.Round(price * 0.95m, 2);
                }

                if (discountPrice >= price)
                {
                    discountPrice = price - 1m;
                }

                discountPrice = Math.Max(discountPrice, 1m);
                if (discountPrice >= price) discountPrice = price - 1m;
                var stock = rnd.Next(10, 200);

                var imagesCount = rnd.Next(3, 6);
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
            var low = new[] { "Accessory", "Cable", "Holder", "Cleaner", "Organiser", "Stylus", "Phone Wallet" };
            var mid = new[] { "TWS", "Neckbands", "Portable", "USB", "Mobile", "Computer", "Wireless" };
            var high = new[] { "Projectors", "Pressure", "Smart", "Kettle", "Blower", "Massagers", "Soundbars" };

            var name = category.ToLowerInvariant();
            if (high.Any(h => name.Contains(h.ToLowerInvariant()))) return 8999m;
            if (name.Contains("watch")) return 5999m;
            if (name.Contains("speaker") || name.Contains("headphone") || name.Contains("earbuds") || name.Contains("tws")) return 3999m;
            if (mid.Any(m => name.Contains(m.ToLowerInvariant()))) return 2999m;
            return 1999m;
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
                    ProductId = Guid.Empty,
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
}
