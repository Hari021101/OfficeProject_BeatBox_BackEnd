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

            if (!existingCategories.ContainsKey("earbuds"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Earbuds", Description = "Earbuds category" };
                context.Categories.Add(newCat);
                existingCategories["earbuds"] = newCat;
            }

            if (!existingCategories.ContainsKey("speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Speakers", Description = "Speakers category" };
                context.Categories.Add(newCat);
                existingCategories["speakers"] = newCat;
            }

            if (!existingCategories.ContainsKey("neckbands"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Neckbands", Description = "Neckbands category" };
                context.Categories.Add(newCat);
                existingCategories["neckbands"] = newCat;
            }

            if (!existingCategories.ContainsKey("gaming"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Gaming", Description = "Gaming category" };
                context.Categories.Add(newCat);
                existingCategories["gaming"] = newCat;
            }

            if (!existingCategories.ContainsKey("power bank"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Power bank", Description = "Power bank category" };
                context.Categories.Add(newCat);
                existingCategories["power bank"] = newCat;
            }

            if (!existingCategories.ContainsKey("trimmer"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Trimmer", Description = "Trimmer category" };
                context.Categories.Add(newCat);
                existingCategories["trimmer"] = newCat;
            }

            if (!existingCategories.ContainsKey("soundbars"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Soundbars", Description = "Soundbars category" };
                context.Categories.Add(newCat);
                existingCategories["soundbars"] = newCat;
            }

            if (!existingCategories.ContainsKey("party speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Party speakers", Description = "Party speakers category" };
                context.Categories.Add(newCat);
                existingCategories["party speakers"] = newCat;
            }

            if (!existingCategories.ContainsKey("portable speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Portable speakers", Description = "Portable speakers category" };
                context.Categories.Add(newCat);
                existingCategories["portable speakers"] = newCat;
            }

            if (!existingCategories.ContainsKey("tws"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Tws", Description = "Tws category" };
                context.Categories.Add(newCat);
                existingCategories["tws"] = newCat;
            }

            if (!existingCategories.ContainsKey("wireless headphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless headphones", Description = "Wireless headphones category" };
                context.Categories.Add(newCat);
                existingCategories["wireless headphones"] = newCat;
            }

            if (!existingCategories.ContainsKey("wired earphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wired earphones", Description = "Wired earphones category" };
                context.Categories.Add(newCat);
                existingCategories["wired earphones"] = newCat;
            }

            if (!existingCategories.ContainsKey("usb speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Usb speakers", Description = "Usb speakers category" };
                context.Categories.Add(newCat);
                existingCategories["usb speakers"] = newCat;
            }

            if (!existingCategories.ContainsKey("conference speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Conference speakers", Description = "Conference speakers category" };
                context.Categories.Add(newCat);
                existingCategories["conference speakers"] = newCat;
            }

            if (!existingCategories.ContainsKey("wireless microphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless microphones", Description = "Wireless microphones category" };
                context.Categories.Add(newCat);
                existingCategories["wireless microphones"] = newCat;
            }
            await context.SaveChangesAsync();

            var existingProducts = await context.Products.Select(p => p.Name).ToListAsync();
            var productsToAdd = new List<Product>();

            if (!existingProducts.Contains("Rockerz Pro ANC 550"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Pro ANC 550",
                    Description = "Experience true audio purity with 40mm dynamic drivers, hybrid ANC, and up to 60 hours of massive playback.",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "auralPrecisionV3",
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

            if (!existingProducts.Contains("Rockerz Wireless 450"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Wireless 450",
                    Description = "Premium wireless headphones with 50-hour battery, signature bass tuning, and ultra-comfortable over-ear cushions.",
                    Price = 2499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones",
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

            if (!existingProducts.Contains("Airdopes Cyber 141"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Airdopes Cyber 141",
                    Description = "BEAST™ mode for 40ms gaming latency, quad ENx™ mics for crystal-clear calls, and a glowing neon charging case.",
                    Price = 2299m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "heroEarbuds",
                    CategoryId = existingCategories["earbuds"].Id,
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

            if (!existingProducts.Contains("BeatBox Smart Capsule"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Smart Capsule",
                    Description = "World's most advanced smart earbuds.",
                    Price = 3999m,
                    DiscountPrice = 2999m,
                    StockQuantity = 100,
                    ImageUrl = "beatboxSmartCapsule",
                    CategoryId = existingCategories["earbuds"].Id,
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

            if (!existingProducts.Contains("Stone Beat Beast 1200"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone Beat Beast 1200",
                    Description = "IPX7 waterproof speaker with dual passive radiators, 14W signature bass, custom RGB ring, and waterproof protection.",
                    Price = 3499m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "heroSpeaker",
                    CategoryId = existingCategories["speakers"].Id,
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

            if (!existingProducts.Contains("Stone Grenade Pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone Grenade Pro",
                    Description = "Compact cylindrical speaker with 360° omnidirectional sound. Perfect for travel, indoor parties, and outdoor picnics.",
                    Price = 2799m,
                    DiscountPrice = 1799m,
                    StockQuantity = 100,
                    ImageUrl = "stoneGrenadePro",
                    CategoryId = existingCategories["speakers"].Id,
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

            if (!existingProducts.Contains("Trip Athletic Neon"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Trip Athletic Neon",
                    Description = "Featherlight neckband with magnetic earbud tips, dual EQ modes, and 30 hours of athletic playback for gym warriors.",
                    Price = 1999m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband",
                    CategoryId = existingCategories["neckbands"].Id,
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

            if (!existingProducts.Contains("Collar Flex Pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Collar Flex Pro",
                    Description = "Budget neckband with legendary ASAP Charge — 10 min charging = 10 hours playback. Perfect daily commute companion.",
                    Price = 1799m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband",
                    CategoryId = existingCategories["neckbands"].Id,
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

            if (!existingProducts.Contains("Immortal Cyber Pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Immortal Cyber Pro",
                    Description = "Dedicated per-zone RGB, 50mm drivers, professional boom mic with flip-to-mute, and virtual 7.1 surround for esports pros.",
                    Price = 2599m,
                    DiscountPrice = 1599m,
                    StockQuantity = 100,
                    ImageUrl = "gamingHeadset",
                    CategoryId = existingCategories["gaming"].Id,
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

            if (!existingProducts.Contains("Immortal Rave 700"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Immortal Rave 700",
                    Description = "Seamlessly switch between wireless 2.4GHz (5ms) and wired 3.5mm modes. 40-hour battery. Detachable boom mic.",
                    Price = 2299m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "gamingHeadset",
                    CategoryId = existingCategories["gaming"].Id,
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

            if (!existingProducts.Contains("Rockerz Club 330"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Club 330",
                    Description = "The fan favourite entry-level neckband with 24-hour battery and IPX5 protection. Perfect for first-time buyers.",
                    Price = 1699m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband",
                    CategoryId = existingCategories["neckbands"].Id,
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

            if (!existingProducts.Contains("Energy Core 10k"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Energy Core 10k",
                    Description = "Ultra-slim 10000mAh power bank with 22.5W fast charging, LED display, and premium aluminum casing. Never run out of juice.",
                    Price = 1999m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "powerBank",
                    CategoryId = existingCategories["power bank"].Id,
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

            if (!existingProducts.Contains("Blade Pro Trimmer"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Blade Pro Trimmer",
                    Description = "Precision beard trimmer with self-sharpening titanium blades, 90 mins runtime, and 20 length settings. Elevate your grooming.",
                    Price = 2499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "trimmer",
                    CategoryId = existingCategories["trimmer"].Id,
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

            if (!existingProducts.Contains("Cinema Pro Soundbars"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cinema Pro Soundbars",
                    Description = "Transform your living room into a cinema with 120W of pure Dolby audio and a wireless subwoofer.",
                    Price = 5999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbar",
                    CategoryId = existingCategories["soundbars"].Id,
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

            if (!existingProducts.Contains("BeatBox Soundbar Pro 5.1"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Pro 5.1",
                    Description = "Immersive 5.1 surround soundbar for the ultimate home cinema experience.",
                    Price = 7999m,
                    DiscountPrice = 6999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarPro51",
                    CategoryId = existingCategories["soundbars"].Id,
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

            if (!existingProducts.Contains("BeatBox Soundbar Elite S9"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Elite S9",
                    Description = "Flagship soundbar with Dolby Atmos and wireless subwoofer for audiophiles.",
                    Price = 9999m,
                    DiscountPrice = 8999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarEliteS9",
                    CategoryId = existingCategories["soundbars"].Id,
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

            if (!existingProducts.Contains("BeatBox Soundbar Mini 2.1"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Mini 2.1",
                    Description = "Compact 2.1 soundbar perfect for small rooms and gaming setups.",
                    Price = 4499m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarMini21",
                    CategoryId = existingCategories["soundbars"].Id,
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

            if (!existingProducts.Contains("BeatBox Gaming Soundbar X"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Gaming Soundbar X",
                    Description = "Gaming-optimized soundbar with virtual 7.1 surround and RGB accents.",
                    Price = 5999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "gamingSoundbarX",
                    CategoryId = existingCategories["soundbars"].Id,
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

            if (!existingProducts.Contains("Party Boom 1500"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Boom 1500",
                    Description = "Massive party speaker with 1500W peak power and built-in disco lights.",
                    Price = 13999m,
                    DiscountPrice = 12999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerHero",
                    CategoryId = existingCategories["party speakers"].Id,
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

            if (!existingProducts.Contains("Party Blast Tower"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Blast Tower",
                    Description = "Tall tower party speaker with a karaoke mic and FM radio.",
                    Price = 15999m,
                    DiscountPrice = 14999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerHero",
                    CategoryId = existingCategories["party speakers"].Id,
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

            if (!existingProducts.Contains("Party Max 2000"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Max 2000",
                    Description = "Professional-grade party speaker for outdoor events and large gatherings.",
                    Price = 19999m,
                    DiscountPrice = 18999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerHero",
                    CategoryId = existingCategories["party speakers"].Id,
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

            if (!existingProducts.Contains("Party Lite Wireless"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Lite Wireless",
                    Description = "Portable party speaker with 12-hour battery and splash resistance.",
                    Price = 8999m,
                    DiscountPrice = 7999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerHero",
                    CategoryId = existingCategories["party speakers"].Id,
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

            if (!existingProducts.Contains("Portable Rugged X3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Rugged X3",
                    Description = "Fully waterproof rugged bluetooth speaker for outdoor adventures.",
                    Price = 3499m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero",
                    CategoryId = existingCategories["portable speakers"].Id,
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

            if (!existingProducts.Contains("Portable Bass Booster"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Bass Booster",
                    Description = "Portable speaker with a 360° passive bass radiator for room-filling sound.",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero",
                    CategoryId = existingCategories["portable speakers"].Id,
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

            if (!existingProducts.Contains("Pocket Mini Speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pocket Mini Speaker",
                    Description = "Ultra-compact pocket speaker that punches well above its size.",
                    Price = 1999m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero",
                    CategoryId = existingCategories["portable speakers"].Id,
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

            if (!existingProducts.Contains("Fabric Portable Speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Fabric Portable Speaker",
                    Description = "Stylish fabric-wrapped portable speaker with rich, warm audio.",
                    Price = 2499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero",
                    CategoryId = existingCategories["portable speakers"].Id,
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

            if (!existingProducts.Contains("TWS Sport Pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Sport Pro",
                    Description = "Sport-tuned TWS earbuds with secure ear hooks and sweat resistance.",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero",
                    CategoryId = existingCategories["tws"].Id,
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

            if (!existingProducts.Contains("TWS ANC Elite"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS ANC Elite",
                    Description = "Premium hybrid ANC TWS earbuds for crystal-clear calls and music.",
                    Price = 4499m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero",
                    CategoryId = existingCategories["tws"].Id,
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

            if (!existingProducts.Contains("TWS Lite Everyday"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Lite Everyday",
                    Description = "Everyday TWS earbuds offering great sound at an unbeatable price.",
                    Price = 1799m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero",
                    CategoryId = existingCategories["tws"].Id,
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

            if (!existingProducts.Contains("TWS Gaming Buds"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Gaming Buds",
                    Description = "Low latency gaming TWS earbuds with a dedicated gaming mode.",
                    Price = 3499m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero",
                    CategoryId = existingCategories["tws"].Id,
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

            if (!existingProducts.Contains("Neckband Pro ANC"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Neckband Pro ANC",
                    Description = "Neckband with active noise cancellation for undisturbed listening.",
                    Price = 2499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "neckbandHero",
                    CategoryId = existingCategories["neckbands"].Id,
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

            if (!existingProducts.Contains("Neckband Sport Flex"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Neckband Sport Flex",
                    Description = "Flexible memory-band neckband that fits every neck comfortably.",
                    Price = 2299m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "neckbandHero",
                    CategoryId = existingCategories["neckbands"].Id,
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

            if (!existingProducts.Contains("ANC Headphones Pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "ANC Headphones Pro",
                    Description = "Industry-leading 45dB ANC headphones with premium leather cushions.",
                    Price = 5999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero",
                    CategoryId = existingCategories["wireless headphones"].Id,
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

            if (!existingProducts.Contains("Wireless Headphones Lite"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Headphones Lite",
                    Description = "Lightweight wireless headphones with great sound for everyday use.",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero",
                    CategoryId = existingCategories["wireless headphones"].Id,
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

            if (!existingProducts.Contains("Studio Headphones X"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Studio Headphones X",
                    Description = "Studio-grade wireless headphones for professional music production.",
                    Price = 7999m,
                    DiscountPrice = 6999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero",
                    CategoryId = existingCategories["wireless headphones"].Id,
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

            if (!existingProducts.Contains("Kids Wireless Headphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Kids Wireless Headphones",
                    Description = "Safe volume-limited wireless headphones designed for children.",
                    Price = 1999m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero",
                    CategoryId = existingCategories["wireless headphones"].Id,
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

            if (!existingProducts.Contains("Wired Bass Boost"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Bass Boost",
                    Description = "Bass-boosted wired earphones with a powerful 12mm driver.",
                    Price = 1499m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero",
                    CategoryId = existingCategories["wired earphones"].Id,
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

            if (!existingProducts.Contains("Wired Pro IEM"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Pro IEM",
                    Description = "Audiophile-grade IEM earphones for detailed, accurate sound reproduction.",
                    Price = 1799m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero",
                    CategoryId = existingCategories["wired earphones"].Id,
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

            if (!existingProducts.Contains("Wired Sport Earphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Sport Earphones",
                    Description = "Sport wired earphones with secure ear hooks and sweat resistance.",
                    Price = 1599m,
                    DiscountPrice = 599m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero",
                    CategoryId = existingCategories["wired earphones"].Id,
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

            if (!existingProducts.Contains("Type-C Wired Earphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Type-C Wired Earphones",
                    Description = "Modern USB-C earphones with a built-in DAC for improved audio quality.",
                    Price = 1899m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero",
                    CategoryId = existingCategories["wired earphones"].Id,
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

            if (!existingProducts.Contains("USB RGB Gaming Speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB RGB Gaming Speakers",
                    Description = "USB gaming speakers with vibrant RGB lighting and punchy bass.",
                    Price = 2299m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "usbSpeakersHero",
                    CategoryId = existingCategories["usb speakers"].Id,
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

            if (!existingProducts.Contains("USB Studio Monitors"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Studio Monitors",
                    Description = "USB studio monitor speakers for accurate audio mixing and content creation.",
                    Price = 2999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "usbSpeakersHero",
                    CategoryId = existingCategories["usb speakers"].Id,
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

            if (!existingProducts.Contains("USB Mini Desktop Speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Mini Desktop Speakers",
                    Description = "No power adapter needed. Just plug into your PC and enjoy clear desktop audio.",
                    Price = 1699m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "usbSpeakersHero",
                    CategoryId = existingCategories["usb speakers"].Id,
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

            if (!existingProducts.Contains("USB Desktop Soundbar"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Desktop Soundbar",
                    Description = "Slim USB soundbar designed to sit neatly under your monitor.",
                    Price = 2499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "usbSpeakersHero",
                    CategoryId = existingCategories["usb speakers"].Id,
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

            if (!existingProducts.Contains("Conference Speaker 360"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Conference Speaker 360",
                    Description = "Omnidirectional conference speaker with 6-mic array for crystal-clear meetings.",
                    Price = 5999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero",
                    CategoryId = existingCategories["conference speakers"].Id,
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

            if (!existingProducts.Contains("Portable Conference Speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Conference Speaker",
                    Description = "Portable conference speakerphone for remote workers and business travelers.",
                    Price = 3999m,
                    DiscountPrice = 2999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero",
                    CategoryId = existingCategories["conference speakers"].Id,
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

            if (!existingProducts.Contains("Conference Elite Hub"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Conference Elite Hub",
                    Description = "AI-powered conference speaker that removes background noise automatically.",
                    Price = 8999m,
                    DiscountPrice = 7999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero",
                    CategoryId = existingCategories["conference speakers"].Id,
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

            if (!existingProducts.Contains("Dual Conference Speakerphone"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dual Conference Speakerphone",
                    Description = "Two daisy-chainable conference speakers for large boardrooms.",
                    Price = 6999m,
                    DiscountPrice = 5999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero",
                    CategoryId = existingCategories["conference speakers"].Id,
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

            if (!existingProducts.Contains("Wireless Handheld Mic"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Handheld Mic",
                    Description = "Professional wireless handheld mic for live performances and events.",
                    Price = 6999m,
                    DiscountPrice = 5999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicHero",
                    CategoryId = existingCategories["wireless microphones"].Id,
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

            if (!existingProducts.Contains("Wireless Lavalier Clip Mic"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Lavalier Clip Mic",
                    Description = "Wireless clip-on lavalier mic for vloggers and content creators.",
                    Price = 4499m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicHero",
                    CategoryId = existingCategories["wireless microphones"].Id,
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

            if (!existingProducts.Contains("Wireless Dual Mic System"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Dual Mic System",
                    Description = "Dual wireless mic system ideal for interviews and two-person podcasts.",
                    Price = 9999m,
                    DiscountPrice = 8999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicHero",
                    CategoryId = existingCategories["wireless microphones"].Id,
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

            if (!existingProducts.Contains("Studio Wireless Condenser"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Studio Wireless Condenser",
                    Description = "Premium wireless condenser microphone for studio-quality recordings.",
                    Price = 13999m,
                    DiscountPrice = 12999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicHero",
                    CategoryId = existingCategories["wireless microphones"].Id,
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
