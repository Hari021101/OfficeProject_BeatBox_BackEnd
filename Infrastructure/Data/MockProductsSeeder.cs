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
            var categoriesAdded = false;

            if (!existingCategories.ContainsKey("wireless headphones"))
            {
                var newCat = new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Wireless Headphones", Description = "Wireless Headphones Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless headphones"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("earbuds"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Earbuds", Description = "Earbuds Category" };
                context.Categories.Add(newCat);
                existingCategories["earbuds"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("bluetooth speakers"))
            {
                var newCat = new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Bluetooth Speakers", Description = "Bluetooth Speakers Category" };
                context.Categories.Add(newCat);
                existingCategories["bluetooth speakers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wireless neckbands"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless Neckbands", Description = "Wireless Neckbands Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless neckbands"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("gaming headsets"))
            {
                var newCat = new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Gaming Headsets", Description = "Gaming Headsets Category" };
                context.Categories.Add(newCat);
                existingCategories["gaming headsets"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("power banks"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Power Banks", Description = "Power Banks Category" };
                context.Categories.Add(newCat);
                existingCategories["power banks"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("grooming trimmers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Grooming Trimmers", Description = "Grooming Trimmers Category" };
                context.Categories.Add(newCat);
                existingCategories["grooming trimmers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("soundbars"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Soundbars", Description = "Soundbars Category" };
                context.Categories.Add(newCat);
                existingCategories["soundbars"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("cables & connectors"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Cables & Connectors", Description = "Cables & Connectors Category" };
                context.Categories.Add(newCat);
                existingCategories["cables & connectors"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("chargers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Chargers", Description = "Chargers Category" };
                context.Categories.Add(newCat);
                existingCategories["chargers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("car accessories"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Car Accessories", Description = "Car Accessories Category" };
                context.Categories.Add(newCat);
                existingCategories["car accessories"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("mobile accessories"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Mobile Accessories", Description = "Mobile Accessories Category" };
                context.Categories.Add(newCat);
                existingCategories["mobile accessories"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("computer accessories"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Computer Accessories", Description = "Computer Accessories Category" };
                context.Categories.Add(newCat);
                existingCategories["computer accessories"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("smart gadgets"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Smart Gadgets", Description = "Smart Gadgets Category" };
                context.Categories.Add(newCat);
                existingCategories["smart gadgets"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("party speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Party Speakers", Description = "Party Speakers Category" };
                context.Categories.Add(newCat);
                existingCategories["party speakers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("portable speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Portable Speakers", Description = "Portable Speakers Category" };
                context.Categories.Add(newCat);
                existingCategories["portable speakers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("tws earbuds"))
            {
                var newCat = new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "TWS Earbuds", Description = "TWS Earbuds Category" };
                context.Categories.Add(newCat);
                existingCategories["tws earbuds"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wired earphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wired Earphones", Description = "Wired Earphones Category" };
                context.Categories.Add(newCat);
                existingCategories["wired earphones"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("usb speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Usb Speakers", Description = "Usb Speakers Category" };
                context.Categories.Add(newCat);
                existingCategories["usb speakers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("conference speakers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Conference Speakers", Description = "Conference Speakers Category" };
                context.Categories.Add(newCat);
                existingCategories["conference speakers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wireless microphones"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless Microphones", Description = "Wireless Microphones Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless microphones"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("gadget cleaners"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Gadget Cleaners", Description = "Gadget Cleaners Category" };
                context.Categories.Add(newCat);
                existingCategories["gadget cleaners"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("phone wallet"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Phone Wallet", Description = "Phone Wallet Category" };
                context.Categories.Add(newCat);
                existingCategories["phone wallet"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("cable organiser"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Cable Organiser", Description = "Cable Organiser Category" };
                context.Categories.Add(newCat);
                existingCategories["cable organiser"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wireless keyboard"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless Keyboard", Description = "Wireless Keyboard Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless keyboard"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wired keyboard"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wired Keyboard", Description = "Wired Keyboard Category" };
                context.Categories.Add(newCat);
                existingCategories["wired keyboard"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("gaming keyboard"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Gaming Keyboard", Description = "Gaming Keyboard Category" };
                context.Categories.Add(newCat);
                existingCategories["gaming keyboard"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wireless mouse"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless Mouse", Description = "Wireless Mouse Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless mouse"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wired mouse"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wired Mouse", Description = "Wired Mouse Category" };
                context.Categories.Add(newCat);
                existingCategories["wired mouse"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("laptop table"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Laptop Table", Description = "Laptop Table Category" };
                context.Categories.Add(newCat);
                existingCategories["laptop table"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("extension board"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Extension Board", Description = "Extension Board Category" };
                context.Categories.Add(newCat);
                existingCategories["extension board"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("projectors"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Projectors", Description = "Projectors Category" };
                context.Categories.Add(newCat);
                existingCategories["projectors"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("lcd writing pads"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Lcd Writing Pads", Description = "Lcd Writing Pads Category" };
                context.Categories.Add(newCat);
                existingCategories["lcd writing pads"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("computer cables"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Computer Cables", Description = "Computer Cables Category" };
                context.Categories.Add(newCat);
                existingCategories["computer cables"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("wireless presenter"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Wireless Presenter", Description = "Wireless Presenter Category" };
                context.Categories.Add(newCat);
                existingCategories["wireless presenter"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("car bluetooth"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Car Bluetooth", Description = "Car Bluetooth Category" };
                context.Categories.Add(newCat);
                existingCategories["car bluetooth"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("car mobile holder"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Car Mobile Holder", Description = "Car Mobile Holder Category" };
                context.Categories.Add(newCat);
                existingCategories["car mobile holder"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("bike mobile holder"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Bike Mobile Holder", Description = "Bike Mobile Holder Category" };
                context.Categories.Add(newCat);
                existingCategories["bike mobile holder"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("car wireless charger"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Car Wireless Charger", Description = "Car Wireless Charger Category" };
                context.Categories.Add(newCat);
                existingCategories["car wireless charger"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("pressure washer"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Pressure Washer", Description = "Pressure Washer Category" };
                context.Categories.Add(newCat);
                existingCategories["pressure washer"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("ear cleaners"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Ear Cleaners", Description = "Ear Cleaners Category" };
                context.Categories.Add(newCat);
                existingCategories["ear cleaners"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("tool kit"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Tool Kit", Description = "Tool Kit Category" };
                context.Categories.Add(newCat);
                existingCategories["tool kit"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("humidifiers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Humidifiers", Description = "Humidifiers Category" };
                context.Categories.Add(newCat);
                existingCategories["humidifiers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("air blower"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Air Blower", Description = "Air Blower Category" };
                context.Categories.Add(newCat);
                existingCategories["air blower"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("timers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Timers", Description = "Timers Category" };
                context.Categories.Add(newCat);
                existingCategories["timers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("massagers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Massagers", Description = "Massagers Category" };
                context.Categories.Add(newCat);
                existingCategories["massagers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("smart sealers"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Smart Sealers", Description = "Smart Sealers Category" };
                context.Categories.Add(newCat);
                existingCategories["smart sealers"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("rechargeable battery"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Rechargeable Battery", Description = "Rechargeable Battery Category" };
                context.Categories.Add(newCat);
                existingCategories["rechargeable battery"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("ssd cards"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Ssd Cards", Description = "Ssd Cards Category" };
                context.Categories.Add(newCat);
                existingCategories["ssd cards"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("pendrives"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Pendrives", Description = "Pendrives Category" };
                context.Categories.Add(newCat);
                existingCategories["pendrives"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("memory cards"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Memory Cards", Description = "Memory Cards Category" };
                context.Categories.Add(newCat);
                existingCategories["memory cards"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("calculators"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Calculators", Description = "Calculators Category" };
                context.Categories.Add(newCat);
                existingCategories["calculators"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("smart watches"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Smart Watches", Description = "Smart Watches Category" };
                context.Categories.Add(newCat);
                existingCategories["smart watches"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("camera"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Camera", Description = "Camera Category" };
                context.Categories.Add(newCat);
                existingCategories["camera"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("latest drops"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Latest Drops", Description = "Latest Drops Category" };
                context.Categories.Add(newCat);
                existingCategories["latest drops"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("trending gear"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Trending Gear", Description = "Trending Gear Category" };
                context.Categories.Add(newCat);
                existingCategories["trending gear"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("upcoming releases"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Upcoming Releases", Description = "Upcoming Releases Category" };
                context.Categories.Add(newCat);
                existingCategories["upcoming releases"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("limited editions"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Limited Editions", Description = "Limited Editions Category" };
                context.Categories.Add(newCat);
                existingCategories["limited editions"] = newCat;
                categoriesAdded = true;
            }

            if (!existingCategories.ContainsKey("smart-ring"))
            {
                var newCat = new Category { Id = Guid.NewGuid(), Name = "Smart-ring", Description = "Smart-ring Category" };
                context.Categories.Add(newCat);
                existingCategories["smart-ring"] = newCat;
                categoriesAdded = true;
            }

            if (categoriesAdded)
            {
                await context.SaveChangesAsync();
            }

            var existingProducts = await context.Products.Select(p => p.Name.ToLower()).ToListAsync();
            var existingProductNames = new HashSet<string>(existingProducts);
            var productsToAdd = new List<Product>();

            if (!existingProductNames.Contains("rockerz pro anc 550"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Pro ANC 550",
                    Description = "Experience true audio purity with 40mm dynamic drivers, hybrid ANC, and up to 60 hours of massive playback.",
                    Price = 7990m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "auralPrecisionV3.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "60 Hours",
                    Color = "Purple",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 6040,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "auralPrecisionV3.png", ColorName = "Purple", ColorCode = "#a820ff", IsPrimary = true },
                        new ProductImage { ImageUrl = "auralPrecisionV3.png", ColorName = "Cyan", ColorCode = "#00f3ff", IsPrimary = false },
                        new ProductImage { ImageUrl = "auralPrecisionV3.png", ColorName = "Black", ColorCode = "#0a0d14", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("rockerz wireless 450"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Wireless 450",
                    Description = "Premium wireless headphones with 50-hour battery, signature bass tuning, and ultra-comfortable over-ear cushions.",
                    Price = 4990m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "50 Hours",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 4320,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Black", ColorCode = "#1a1a2e", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "White", ColorCode = "#e8e8f0", IsPrimary = false },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Blue", ColorCode = "#0d6efd", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("airdopes cyber 141"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Airdopes Cyber 141",
                    Description = "BEAST™ mode for 40ms gaming latency, quad ENx™ mics for crystal-clear calls, and a glowing neon charging case.",
                    Price = 4490m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "heroEarbuds.png",
                    CategoryId = existingCategories["earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "42 Hours Total",
                    Color = "Cyan",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 4780,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "Cyan", ColorCode = "#00f3ff", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "Purple", ColorCode = "#a820ff", IsPrimary = false },
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "Grey", ColorCode = "#8496ae", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox smart capsule"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Smart Capsule",
                    Description = "World's first OLED touchscreen charging case. Adjust EQ, monitor battery, toggle ANC with a swipe on the case.",
                    Price = 9990m,
                    DiscountPrice = 2999m,
                    StockQuantity = 100,
                    ImageUrl = "beatboxSmartCapsule.png",
                    CategoryId = existingCategories["earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "38 Hours",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 2060,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "beatboxSmartCapsule.png", ColorName = "Black", ColorCode = "#0a0a0a", IsPrimary = true },
                        new ProductImage { ImageUrl = "beatboxSmartCapsule.png", ColorName = "Rose Gold", ColorCode = "#b76e79", IsPrimary = false },
                        new ProductImage { ImageUrl = "beatboxSmartCapsule.png", ColorName = "Purple", ColorCode = "#8b5cf6", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("stone beat beast 1200"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone Beat Beast 1200",
                    Description = "IPX7 waterproof speaker with dual passive radiators, 14W signature bass, custom RGB ring, and waterproof protection.",
                    Price = 6990m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "heroSpeaker.png",
                    CategoryId = existingCategories["bluetooth speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "14 Hours",
                    Color = "Carbon",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 2710,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroSpeaker.png", ColorName = "Carbon", ColorCode = "#1a2238", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroSpeaker.png", ColorName = "Blue", ColorCode = "#0d6efd", IsPrimary = false },
                        new ProductImage { ImageUrl = "heroSpeaker.png", ColorName = "Red", ColorCode = "#dc3545", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("stone grenade pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone Grenade Pro",
                    Description = "Compact cylindrical speaker with 360° omnidirectional sound. Perfect for travel, indoor parties, and outdoor picnics.",
                    Price = 4990m,
                    DiscountPrice = 1799m,
                    StockQuantity = 100,
                    ImageUrl = "stoneGrenadePro.png",
                    CategoryId = existingCategories["bluetooth speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "10 Hours",
                    Color = "Green",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 1890,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "stoneGrenadePro.png", ColorName = "Green", ColorCode = "#556b2f", IsPrimary = true },
                        new ProductImage { ImageUrl = "stoneGrenadePro.png", ColorName = "Black", ColorCode = "#1a1a1a", IsPrimary = false },
                        new ProductImage { ImageUrl = "stoneGrenadePro.png", ColorName = "Blue", ColorCode = "#007bff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("trip athletic neon"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Trip Athletic Neon",
                    Description = "Featherlight neckband with magnetic earbud tips, dual EQ modes, and 30 hours of athletic playback for gym warriors.",
                    Price = 2990m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "30 Hours",
                    Color = "Neon Green",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 3615,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Neon Green", ColorCode = "#39ff14", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Cyan", ColorCode = "#00f3ff", IsPrimary = false },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Yellow", ColorCode = "#ffd700", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("collar flex pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Collar Flex Pro",
                    Description = "Budget neckband with legendary ASAP Charge — 10 min charging = 10 hours playback. Perfect daily commute companion.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "24 Hours",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 2670,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Black", ColorCode = "#0a0a0a", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Blue", ColorCode = "#0d6efd", IsPrimary = false },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Red", ColorCode = "#dc3545", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("immortal cyber pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Immortal Cyber Pro",
                    Description = "Dedicated per-zone RGB, 50mm drivers, professional boom mic with flip-to-mute, and virtual 7.1 surround for esports pros.",
                    Price = 4999m,
                    DiscountPrice = 1599m,
                    StockQuantity = 100,
                    ImageUrl = "gamingHeadset.png",
                    CategoryId = existingCategories["gaming headsets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Neon Green",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4110,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "gamingHeadset.png", ColorName = "Neon Green", ColorCode = "#39ff14", IsPrimary = true },
                        new ProductImage { ImageUrl = "gamingHeadset.png", ColorName = "Purple", ColorCode = "#a820ff", IsPrimary = false },
                        new ProductImage { ImageUrl = "gamingHeadset.png", ColorName = "Cyan", ColorCode = "#00f3ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("immortal rave 700"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Immortal Rave 700",
                    Description = "Seamlessly switch between wireless 2.4GHz (5ms) and wired 3.5mm modes. 40-hour battery. Detachable boom mic.",
                    Price = 3999m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "gamingHeadset.png",
                    CategoryId = existingCategories["gaming headsets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "40 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3205,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "gamingHeadset.png", ColorName = "Black", ColorCode = "#0d0d0d", IsPrimary = true },
                        new ProductImage { ImageUrl = "gamingHeadset.png", ColorName = "White", ColorCode = "#f0f0f5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("rockerz club 330"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz Club 330",
                    Description = "The fan favourite entry-level neckband with 24-hour battery and IPX5 protection. Perfect for first-time buyers.",
                    Price = 1799m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "24 Hours",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 5520,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Black", ColorCode = "#1a1a1a", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Blue", ColorCode = "#87ceeb", IsPrimary = false },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Red", ColorCode = "#722f37", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm 111"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm 111",
                    Description = "Premium ANC earbuds with 35-hour battery and CVC 8.0 dual-mic for crystal-clear calls in any environment.",
                    Price = 2999m,
                    DiscountPrice = 1099m,
                    StockQuantity = 100,
                    ImageUrl = "heroEarbuds.png",
                    CategoryId = existingCategories["earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "35 Hours",
                    Color = "Grey",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 3435,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "Grey", ColorCode = "#6b7280", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "Black", ColorCode = "#111827", IsPrimary = false },
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "White", ColorCode = "#f9fafb", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("energy core 10k"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Energy Core 10k",
                    Description = "Ultra-slim 10000mAh power bank with 22.5W fast charging, LED display, and premium aluminum casing. Never run out of juice.",
                    Price = 2499m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "powerBank.png",
                    CategoryId = existingCategories["power banks"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "powerBank.png", ColorName = "Black", ColorCode = "#1a1a1a", IsPrimary = true },
                        new ProductImage { ImageUrl = "powerBank.png", ColorName = "White", ColorCode = "#f9fafb", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("blade pro trimmer"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Blade Pro Trimmer",
                    Description = "Precision beard trimmer with self-sharpening titanium blades, 90 mins runtime, and 20 length settings. Elevate your grooming.",
                    Price = 3499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "trimmer.png",
                    CategoryId = existingCategories["grooming trimmers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "90 Mins",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1575,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "trimmer.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true },
                        new ProductImage { ImageUrl = "trimmer.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("cinema pro soundbars"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cinema Pro Soundbars",
                    Description = "Transform your living room into a cinema with 120W of pure Dolby audio and a wireless subwoofer.",
                    Price = 12999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbar.png",
                    CategoryId = existingCategories["soundbars"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "soundbar.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("premium braided cables"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Premium Braided Cables",
                    Description = "Ultra-durable nylon braided cables supporting 60W Power Delivery fast charging.",
                    Price = 999m,
                    DiscountPrice = 299m,
                    StockQuantity = 100,
                    ImageUrl = "premiumCables.png",
                    CategoryId = existingCategories["cables & connectors"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "premiumCables.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "premiumCables.png", ColorName = "Red", ColorCode = "#ff0000", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("magnetic wireless charger"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Magnetic Wireless Charger",
                    Description = "Sleek metallic wireless charging pad with strong magnetic alignment for instantaneous 15W charging.",
                    Price = 3499m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessCharger.png",
                    CategoryId = existingCategories["chargers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessCharger.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("auto grip car charger"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Auto Grip Car Charger",
                    Description = "Premium dual-port car charger to fast-charge two devices simultaneously while on the move.",
                    Price = 2499m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "carCharger.png",
                    CategoryId = existingCategories["car accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "carCharger.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("flexi mobile holder"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Flexi Mobile Holder",
                    Description = "Ergonomic aluminum mobile holder for your desk. Perfect for video calls and content consumption.",
                    Price = 999m,
                    DiscountPrice = 399m,
                    StockQuantity = 100,
                    ImageUrl = "mobileHolder.png",
                    CategoryId = existingCategories["mobile accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "mobileHolder.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("ergo laptop stand"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Ergo Laptop Stand",
                    Description = "Premium adjustable aluminum laptop stand to improve your posture and device cooling.",
                    Price = 3999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "laptopStand.png",
                    CategoryId = existingCategories["computer accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "laptopStand.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("multi-port usb hub"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Multi-Port USB Hub",
                    Description = "Expand your laptop connectivity with this sleek 7-in-1 Type-C hub adapter.",
                    Price = 4999m,
                    DiscountPrice = 1899m,
                    StockQuantity = 100,
                    ImageUrl = "usbHub.png",
                    CategoryId = existingCategories["computer accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbHub.png", ColorName = "Grey", ColorCode = "#808080", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("pro laptop bags"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pro Laptop Bags",
                    Description = "Professional water-resistant laptop backpack with anti-theft compartments and ergonomic padding.",
                    Price = 5999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "laptopBag.png",
                    CategoryId = existingCategories["computer accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 725,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "laptopBag.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart tyre inflator"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Tyre Inflator",
                    Description = "Portable digital tyre inflator with auto shut-off and built-in LED flashlight.",
                    Price = 6999m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "tyreInflator.png",
                    CategoryId = existingCategories["car accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "4000mAh",
                    Color = "Dark Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 440,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "tyreInflator.png", ColorName = "Dark Grey", ColorCode = "#404040", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("cordless vacuum cleaner"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cordless Vacuum Cleaner",
                    Description = "Sleek cordless handheld vacuum cleaner perfect for keeping your car and desk spotless.",
                    Price = 5999m,
                    DiscountPrice = 2199m,
                    StockQuantity = 100,
                    ImageUrl = "vacuumCleaner.png",
                    CategoryId = existingCategories["car accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "30 Mins Runtime",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 560,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "vacuumCleaner.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("ionic hair dryer"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Ionic Hair Dryer",
                    Description = "Premium ionic hair dryer that reduces frizz and dries hair quickly without heat damage.",
                    Price = 4999m,
                    DiscountPrice = 1799m,
                    StockQuantity = 100,
                    ImageUrl = "hairDryer.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Magenta",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "hairDryer.png", ColorName = "Magenta", ColorCode = "#ff00ff", IsPrimary = true },
                        new ProductImage { ImageUrl = "hairDryer.png", ColorName = "Dark Grey", ColorCode = "#404040", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart electric kettle"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Electric Kettle",
                    Description = "Sleek matte black electric kettle with real-time temperature display and keep-warm functionality.",
                    Price = 3999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "electricKettle.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Matte Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 780,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "electricKettle.png", ColorName = "Matte Black", ColorCode = "#202020", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart location tracker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Location tracker",
                    Description = "Small premium smart tracker tag to keep tabs on your keys, wallet, or luggage.",
                    Price = 2499m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "beatboxSmartTagImage.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "1 Year Replaceable",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "beatboxSmartTagImage.png", ColorName = "White", ColorCode = "#ffffff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("breeze portable fans"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Breeze Portable Fans",
                    Description = "Compact and powerful portable fan to keep you cool on the go.",
                    Price = 1499m,
                    DiscountPrice = 599m,
                    StockQuantity = 100,
                    ImageUrl = "portableFan.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "2000mAh",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableFan.png", ColorName = "White", ColorCode = "#ffffff", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableFan.png", ColorName = "Pink", ColorCode = "#ffc0cb", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("keyboard and mouse set"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Keyboard And Mouse Set",
                    Description = "Sleek wireless keyboard and mouse combo with silent keys and ergonomic design.",
                    Price = 3499m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "keyboardMouse.png",
                    CategoryId = existingCategories["computer accessories"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "12 Months",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "keyboardMouse.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("pro selfie stick"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pro Selfie Stick",
                    Description = "Extendable aluminum selfie stick with a detachable Bluetooth remote for perfect group shots.",
                    Price = 1299m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "mobileHolder.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Bluetooth 5.0",
                    IsFeatured = true,
                    SoldCount = 1150,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "mobileHolder.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("tactical flashlight"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tactical Flashlight",
                    Description = "Ultra-bright tactical flashlight with adjustable focus and SOS mode for emergencies.",
                    Price = 1999m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "portableFan.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Rechargeable",
                    Color = "Military Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableFan.png", ColorName = "Military Black", ColorCode = "#1a1a1a", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("precision stylus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Precision Stylus",
                    Description = "High-precision active stylus pen with palm rejection for smooth drawing and note-taking.",
                    Price = 2499m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart gadgets"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "12 Hours",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "White", ColorCode = "#ffffff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("mega party speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mega Party Speaker",
                    Description = "Turn any space into a club with 250W thunder bass and dynamic RGB party lights.",
                    Price = 19999m,
                    DiscountPrice = 9999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeaker.png",
                    CategoryId = existingCategories["party speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "10 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4200,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partySpeaker.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("mini portable speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mini Portable Speaker",
                    Description = "Compact 10W portable speaker with rich bass and full IPX7 waterproofing.",
                    Price = 2999m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "heroSpeaker.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "12 Hours",
                    Color = "Blue",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroSpeaker.png", ColorName = "Blue", ColorCode = "#0000ff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("true wireless tws"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "True Wireless TWS",
                    Description = "Reliable true wireless earbuds with 50 hours of total playback and low latency gaming mode.",
                    Price = 3999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "heroEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "50 Hours Total",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroEarbuds.png", ColorName = "White", ColorCode = "#ffffff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("elite wireless headphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Elite Wireless Headphones",
                    Description = "Studio-grade wireless headphones with hybrid active noise cancellation.",
                    Price = 5999m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "40 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1575,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("bass wired earphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Bass Wired Earphones",
                    Description = "Classic wired earphones with deep bass, an in-line mic, and tangle-free braided cables.",
                    Price = 999m,
                    DiscountPrice = 399m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphones.png",
                    CategoryId = existingCategories["wired earphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Red",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 6000,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredEarphones.png", ColorName = "Red", ColorCode = "#ff0000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("desk usb speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Desk USB Speakers",
                    Description = "Compact plug-and-play USB speakers with modern angles and subtle RGB underglow for your desk.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "usbSpeakers.png",
                    CategoryId = existingCategories["usb speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbSpeakers.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("pro conference speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pro Conference Speakers",
                    Description = "Premium conference room speakerphone puck with 360-degree voice pickup and touch controls.",
                    Price = 8999m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakers.png",
                    CategoryId = existingCategories["conference speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "15 Hours",
                    Color = "Silver",
                    Connectivity = "Bluetooth/USB",
                    IsFeatured = true,
                    SoldCount = 425,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "conferenceSpeakers.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("dual wireless microphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dual Wireless Microphones",
                    Description = "Professional dual wireless microphone set with digital receiver for broadcasting and vlogging.",
                    Price = 11999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicrophones.png",
                    CategoryId = existingCategories["wireless microphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "8 Hours/Mic",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 325,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMicrophones.png", ColorName = "Black", ColorCode = "#000000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("gadget cleaners kit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Gadget Cleaners Kit",
                    Description = "7-in-1 gadget cleaning kit for keyboards, earbuds, and screens.",
                    Price = 599m,
                    DiscountPrice = 299m,
                    StockQuantity = 100,
                    ImageUrl = "vacuumCleaner.png",
                    CategoryId = existingCategories["gadget cleaners"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "vacuumCleaner.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("magnetic phone wallet"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Magnetic Phone Wallet",
                    Description = "Premium vegan leather magnetic phone wallet that securely holds up to 3 cards.",
                    Price = 999m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "phoneWallet.png",
                    CategoryId = existingCategories["phone wallet"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "phoneWallet.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("magnetic cable organiser"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Magnetic Cable Organiser",
                    Description = "Keep your workspace tidy with these magnetic silicone cable organizers.",
                    Price = 499m,
                    DiscountPrice = 199m,
                    StockQuantity = 100,
                    ImageUrl = "cableOrganiser.png",
                    CategoryId = existingCategories["cable organiser"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "cableOrganiser.png", ColorName = "Grey", ColorCode = "#888", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("pro wireless keyboard"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pro Wireless Keyboard",
                    Description = "Slim multi-device wireless keyboard for seamless switching between PC and tablet.",
                    Price = 3999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessKeyboard.png",
                    CategoryId = existingCategories["wireless keyboard"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessKeyboard.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("classic wired keyboard"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Classic Wired Keyboard",
                    Description = "Durable and spill-resistant wired keyboard for everyday office use.",
                    Price = 999m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "wiredKeyboard.png",
                    CategoryId = existingCategories["wired keyboard"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1000,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredKeyboard.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("rgb gaming keyboard"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "RGB Gaming Keyboard",
                    Description = "Tactile mechanical gaming keyboard with per-key RGB lighting.",
                    Price = 5999m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "gamingKeyboard.png",
                    CategoryId = existingCategories["gaming keyboard"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "gamingKeyboard.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("ergo wireless mouse"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Ergo Wireless Mouse",
                    Description = "Ergonomic wireless mouse with silent clicks and 12-month battery life.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMouse.png",
                    CategoryId = existingCategories["wireless mouse"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMouse.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("precision wired mouse"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Precision Wired Mouse",
                    Description = "Reliable optical wired mouse for smooth and precise tracking.",
                    Price = 699m,
                    DiscountPrice = 299m,
                    StockQuantity = 100,
                    ImageUrl = "wiredMouse.png",
                    CategoryId = existingCategories["wired mouse"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1700,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredMouse.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("foldable laptop table"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Foldable Laptop Table",
                    Description = "Sturdy foldable laptop table perfect for working from bed or the couch.",
                    Price = 1999m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "laptopTable.png",
                    CategoryId = existingCategories["laptop table"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Wood",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "laptopTable.png", ColorName = "Wood", ColorCode = "#deb887", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart extension board"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Extension Board",
                    Description = "Surge-protected extension board with 4 AC sockets and 2 fast-charging USB ports.",
                    Price = 1499m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "extensionBoard.png",
                    CategoryId = existingCategories["extension board"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "extensionBoard.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("mini hd projectors"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mini HD Projectors",
                    Description = "Compact mini projector to bring the cinema experience to your bedroom.",
                    Price = 12999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "projector.png",
                    CategoryId = existingCategories["projectors"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "projector.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("digital lcd writing pads"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Digital LCD Writing Pads",
                    Description = "Eco-friendly LCD writing pad for notes, doodles, and lists without wasting paper.",
                    Price = 999m,
                    DiscountPrice = 399m,
                    StockQuantity = 100,
                    ImageUrl = "lcdWritingPad.png",
                    CategoryId = existingCategories["lcd writing pads"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "N/A",
                    Color = "Blue",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "lcdWritingPad.png", ColorName = "Blue", ColorCode = "#00f", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("high-speed computer cables"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "High-Speed Computer Cables",
                    Description = "Ultra-fast CAT8 ethernet computer cables for zero-lag gaming and streaming.",
                    Price = 1299m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "computerCables.png",
                    CategoryId = existingCategories["computer cables"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "computerCables.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("laser wireless presenter"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Laser Wireless Presenter",
                    Description = "Sleek wireless presenter remote with a bright red laser pointer for impactful meetings.",
                    Price = 1499m,
                    DiscountPrice = 599m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessPresenter.png",
                    CategoryId = existingCategories["wireless presenter"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 950,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessPresenter.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("car bluetooth receiver"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Car Bluetooth Receiver",
                    Description = "Upgrade your old car stereo to wireless with this compact Bluetooth AUX receiver.",
                    Price = 999m,
                    DiscountPrice = 399m,
                    StockQuantity = 100,
                    ImageUrl = "carBluetoothAdapterImage.png",
                    CategoryId = existingCategories["car bluetooth"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wireless",
                    IsFeatured = true,
                    SoldCount = 1650,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "carBluetoothAdapterImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("dash car mobile holder"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dash Car Mobile Holder",
                    Description = "Secure suction cup car mobile holder with a telescopic arm for optimal navigation viewing.",
                    Price = 1299m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "mobileHolder.png",
                    CategoryId = existingCategories["car mobile holder"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "mobileHolder.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("alloy bike mobile holder"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alloy Bike Mobile Holder",
                    Description = "Rugged aluminum bike mobile holder that keeps your phone secure on the bumpiest trails.",
                    Price = 1499m,
                    DiscountPrice = 599m,
                    StockQuantity = 100,
                    ImageUrl = "mobileHolder.png",
                    CategoryId = existingCategories["bike mobile holder"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "mobileHolder.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("maggrip car wireless charger"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "MagGrip Car Wireless Charger",
                    Description = "Futuristic auto-clamping car wireless charger that detects your phone and securely grips it.",
                    Price = 3999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessCharger.png",
                    CategoryId = existingCategories["car wireless charger"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessCharger.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("high pressure washer"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "High Pressure Washer",
                    Description = "Heavy-duty high pressure washer perfect for cleaning cars, bikes, and driveways effortlessly.",
                    Price = 12999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "vacuumCleaner.png",
                    CategoryId = existingCategories["pressure washer"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Yellow",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "vacuumCleaner.png", ColorName = "Yellow", ColorCode = "#ff0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart visual ear cleaners"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Visual Ear Cleaners",
                    Description = "High-tech visual ear cleaner with an integrated 1080p camera that syncs directly to your phone.",
                    Price = 2499m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "trimmer.png",
                    CategoryId = existingCategories["ear cleaners"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "trimmer.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("46-piece tool kit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "46-Piece Tool Kit",
                    Description = "Comprehensive 46-piece socket and wrench tool kit forged from premium chrome vanadium steel.",
                    Price = 2499m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "laptopBag.png",
                    CategoryId = existingCategories["tool kit"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Red",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "laptopBag.png", ColorName = "Red", ColorCode = "#f00", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("aroma diffuser humidifiers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Aroma Diffuser Humidifiers",
                    Description = "Ultrasonic aroma diffuser humidifier with soothing RGB lighting for a relaxing room ambiance.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "electricKettle.png",
                    CategoryId = existingCategories["humidifiers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Wood",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "electricKettle.png", ColorName = "Wood", ColorCode = "#deb887", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("heavy duty air blower"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Heavy Duty Air Blower",
                    Description = "High-velocity 500W air blower for cleaning PC internals and clearing dust from tight spaces.",
                    Price = 2499m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "portableFan.png",
                    CategoryId = existingCategories["air blower"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Blue",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableFan.png", ColorName = "Blue", ColorCode = "#00f", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("digital pomodoro timers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Digital Pomodoro Timers",
                    Description = "Sleek digital rotating timer with a magnetic back, perfect for the Pomodoro technique and cooking.",
                    Price = 999m,
                    DiscountPrice = 399m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["timers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("deep tissue massagers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Deep Tissue Massagers",
                    Description = "Professional grade percussion massage gun to relieve muscle tension and accelerate recovery.",
                    Price = 4999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "trimmer.png",
                    CategoryId = existingCategories["massagers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "trimmer.png", ColorName = "Grey", ColorCode = "#888", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("mini smart sealers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mini smart Sealers",
                    Description = "Compact mini heat sealer to easily reseal snack bags and keep food fresh for longer.",
                    Price = 799m,
                    DiscountPrice = 299m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart sealers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "AA Operated",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("aa rechargeable battery set"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "AA Rechargeable Battery Set",
                    Description = "Pack of 4 high-capacity Ni-MH rechargeable batteries. Stop buying disposable batteries!",
                    Price = 1499m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "powerBank.png",
                    CategoryId = existingCategories["rechargeable battery"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Green",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "powerBank.png", ColorName = "Green", ColorCode = "#0f0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox soundbar pro 5.1"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Pro 5.1",
                    Description = "Immersive 5.1 surround soundbar for the ultimate home cinema experience.",
                    Price = 14999m,
                    DiscountPrice = 6999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarPro51.png",
                    CategoryId = existingCategories["soundbars"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "soundbarPro51.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox soundbar elite s9"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Elite S9",
                    Description = "Flagship soundbar with Dolby Atmos and wireless subwoofer for audiophiles.",
                    Price = 19999m,
                    DiscountPrice = 8999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarEliteS9.png",
                    CategoryId = existingCategories["soundbars"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "soundbarEliteS9.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox soundbar mini 2.1"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Soundbar Mini 2.1",
                    Description = "Compact 2.1 soundbar perfect for small rooms and gaming setups.",
                    Price = 7999m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarMini21.png",
                    CategoryId = existingCategories["soundbars"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "soundbarMini21.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox gaming soundbar x"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Gaming Soundbar X",
                    Description = "Gaming-optimized soundbar with virtual 7.1 surround and RGB accents.",
                    Price = 10999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "gamingSoundbarX.png",
                    CategoryId = existingCategories["soundbars"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "gamingSoundbarX.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("party boom 1500"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Boom 1500",
                    Description = "Massive party speaker with 1500W peak power and built-in disco lights.",
                    Price = 24999m,
                    DiscountPrice = 12999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerImage.png",
                    CategoryId = existingCategories["party speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "8 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partySpeakerImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("party blast tower"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Blast Tower",
                    Description = "Tall tower party speaker with a karaoke mic and FM radio.",
                    Price = 29999m,
                    DiscountPrice = 14999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerImage.png",
                    CategoryId = existingCategories["party speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partySpeakerImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("party max 2000"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Max 2000",
                    Description = "Professional-grade party speaker for outdoor events and large gatherings.",
                    Price = 39999m,
                    DiscountPrice = 18999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerImage.png",
                    CategoryId = existingCategories["party speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partySpeakerImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("party lite wireless"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Party Lite Wireless",
                    Description = "Portable party speaker with 12-hour battery and splash resistance.",
                    Price = 16999m,
                    DiscountPrice = 7999m,
                    StockQuantity = 100,
                    ImageUrl = "partySpeakerImage.png",
                    CategoryId = existingCategories["party speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "12 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partySpeakerImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("portable rugged x3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Rugged X3",
                    Description = "Fully waterproof rugged bluetooth speaker for outdoor adventures.",
                    Price = 5999m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "24 Hours",
                    Color = "Green",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Green", ColorCode = "#228b22", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("portable bass booster"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Bass Booster",
                    Description = "Portable speaker with a 360° passive bass radiator for room-filling sound.",
                    Price = 4499m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "16 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3200,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("pocket mini speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Pocket Mini Speaker",
                    Description = "Ultra-compact pocket speaker that punches well above its size.",
                    Price = 2499m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "8 Hours",
                    Color = "Blue",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Blue", ColorCode = "#00f", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("fabric portable speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Fabric Portable Speaker",
                    Description = "Stylish fabric-wrapped portable speaker with rich, warm audio.",
                    Price = 3499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "20 Hours",
                    Color = "Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1950,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Grey", ColorCode = "#888", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("tws sport pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Sport Pro",
                    Description = "Sport-tuned TWS earbuds with secure ear hooks and sweat resistance.",
                    Price = 4999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "twsHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("tws anc elite"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS ANC Elite",
                    Description = "Premium hybrid ANC TWS earbuds for crystal-clear calls and music.",
                    Price = 7999m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "twsEarbudsProImage.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "twsEarbudsProImage.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("tws lite everyday"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Lite Everyday",
                    Description = "Everyday TWS earbuds offering great sound at an unbeatable price.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 5500,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "twsHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("tws gaming buds"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TWS Gaming Buds",
                    Description = "Low latency gaming TWS earbuds with a dedicated gaming mode.",
                    Price = 5999m,
                    DiscountPrice = 2499m,
                    StockQuantity = 100,
                    ImageUrl = "twsHero.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "twsHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("neckband pro anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Neckband Pro ANC",
                    Description = "Neckband with active noise cancellation for undisturbed listening.",
                    Price = 3499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "neckbandHero.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2800,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "neckbandHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("neckband sport flex"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Neckband Sport Flex",
                    Description = "Flexible memory-band neckband that fits every neck comfortably.",
                    Price = 2999m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "neckbandHero.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Blue",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2150,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "neckbandHero.png", ColorName = "Blue", ColorCode = "#00f", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("anc headphones pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "ANC Headphones Pro",
                    Description = "Industry-leading 45dB ANC headphones with premium leather cushions.",
                    Price = 10999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessHeadphonesHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wireless headphones lite"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Headphones Lite",
                    Description = "Lightweight wireless headphones with great sound for everyday use.",
                    Price = 4999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessHeadphonesHero.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("studio headphones x"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Studio Headphones X",
                    Description = "Studio-grade wireless headphones for professional music production.",
                    Price = 14999m,
                    DiscountPrice = 6999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 750,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessHeadphonesHero.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("kids wireless headphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Kids Wireless Headphones",
                    Description = "Safe volume-limited wireless headphones designed for children.",
                    Price = 2499m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessHeadphonesHero.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Pink",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessHeadphonesHero.png", ColorName = "Pink", ColorCode = "#ffc0cb", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wired bass boost"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Bass Boost",
                    Description = "Bass-boosted wired earphones with a powerful 12mm driver.",
                    Price = 1199m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero.png",
                    CategoryId = existingCategories["wired earphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 6000,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredEarphonesHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wired pro iem"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Pro IEM",
                    Description = "Audiophile-grade IEM earphones for detailed, accurate sound reproduction.",
                    Price = 1999m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero.png",
                    CategoryId = existingCategories["wired earphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredEarphonesHero.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wired sport earphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wired Sport Earphones",
                    Description = "Sport wired earphones with secure ear hooks and sweat resistance.",
                    Price = 1499m,
                    DiscountPrice = 599m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero.png",
                    CategoryId = existingCategories["wired earphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Red",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3350,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredEarphonesHero.png", ColorName = "Red", ColorCode = "#f00", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("type-c wired earphones"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Type-C Wired Earphones",
                    Description = "Modern USB-C earphones with a built-in DAC for improved audio quality.",
                    Price = 1999m,
                    DiscountPrice = 899m,
                    StockQuantity = 100,
                    ImageUrl = "wiredEarphonesHero.png",
                    CategoryId = existingCategories["wired earphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1950,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wiredEarphonesHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("usb rgb gaming speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB RGB Gaming Speakers",
                    Description = "USB gaming speakers with vibrant RGB lighting and punchy bass.",
                    Price = 2999m,
                    DiscountPrice = 1299m,
                    StockQuantity = 100,
                    ImageUrl = "usbGamingSpeakersImage.png",
                    CategoryId = existingCategories["usb speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2150,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbGamingSpeakersImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("usb studio monitors"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Studio Monitors",
                    Description = "USB studio monitor speakers for accurate audio mixing and content creation.",
                    Price = 4499m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "usbGamingSpeakersImage.png",
                    CategoryId = existingCategories["usb speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbGamingSpeakersImage.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("usb mini desktop speakers"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Mini Desktop Speakers",
                    Description = "No power adapter needed. Just plug into your PC and enjoy clear desktop audio.",
                    Price = 1699m,
                    DiscountPrice = 699m,
                    StockQuantity = 100,
                    ImageUrl = "usbGamingSpeakersImage.png",
                    CategoryId = existingCategories["usb speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 3800,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbGamingSpeakersImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("usb desktop soundbar"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "USB Desktop Soundbar",
                    Description = "Slim USB soundbar designed to sit neatly under your monitor.",
                    Price = 3499m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "usbGamingSpeakersImage.png",
                    CategoryId = existingCategories["usb speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "usbGamingSpeakersImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("conference speaker 360"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Conference Speaker 360",
                    Description = "Omnidirectional conference speaker with 6-mic array for crystal-clear meetings.",
                    Price = 11999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero.png",
                    CategoryId = existingCategories["conference speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 700,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "conferenceSpeakerHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("portable conference speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable Conference Speaker",
                    Description = "Portable conference speakerphone for remote workers and business travelers.",
                    Price = 6999m,
                    DiscountPrice = 2999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero.png",
                    CategoryId = existingCategories["conference speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "10 Hours",
                    Color = "Grey",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1150,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "conferenceSpeakerHero.png", ColorName = "Grey", ColorCode = "#888", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("conference elite hub"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Conference Elite Hub",
                    Description = "AI-powered conference speaker that removes background noise automatically.",
                    Price = 17999m,
                    DiscountPrice = 7999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero.png",
                    CategoryId = existingCategories["conference speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "conferenceSpeakerHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("dual conference speakerphone"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dual Conference Speakerphone",
                    Description = "Two daisy-chainable conference speakers for large boardrooms.",
                    Price = 13999m,
                    DiscountPrice = 5999m,
                    StockQuantity = 100,
                    ImageUrl = "conferenceSpeakerHero.png",
                    CategoryId = existingCategories["conference speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "conferenceSpeakerHero.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wireless handheld mic"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Handheld Mic",
                    Description = "Professional wireless handheld mic for live performances and events.",
                    Price = 12999m,
                    DiscountPrice = 5999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicImage.png",
                    CategoryId = existingCategories["wireless microphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "10 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMicImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wireless lavalier clip mic"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Lavalier Clip Mic",
                    Description = "Wireless clip-on lavalier mic for vloggers and content creators.",
                    Price = 7999m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicImage.png",
                    CategoryId = existingCategories["wireless microphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "8 Hours",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1950,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMicImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("wireless dual mic system"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Wireless Dual Mic System",
                    Description = "Dual wireless mic system ideal for interviews and two-person podcasts.",
                    Price = 19999m,
                    DiscountPrice = 8999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicImage.png",
                    CategoryId = existingCategories["wireless microphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 475,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMicImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("studio wireless condenser"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Studio Wireless Condenser",
                    Description = "Premium wireless condenser microphone for studio-quality recordings.",
                    Price = 27999m,
                    DiscountPrice = 12999m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessMicImage.png",
                    CategoryId = existingCategories["wireless microphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 300,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessMicImage.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ssd pro 1tb"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox SSD Pro 1TB",
                    Description = "Lightning fast 1TB NVMe Gen4 SSD for ultimate gaming and productivity.",
                    Price = 10999m,
                    DiscountPrice = 5999m,
                    StockQuantity = 100,
                    ImageUrl = "ssdDriveImage.png",
                    CategoryId = existingCategories["ssd cards"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1550,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "ssdDriveImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox flash pendrive"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Flash Pendrive",
                    Description = "Compact and durable metal body pendrive.",
                    Price = 1999m,
                    DiscountPrice = 999m,
                    StockQuantity = 100,
                    ImageUrl = "pendriveFlashImage.png",
                    CategoryId = existingCategories["pendrives"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "pendriveFlashImage.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultra microsd"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultra MicroSD",
                    Description = "High-speed MicroSD card perfect for 4K video recording and smartphones.",
                    Price = 2999m,
                    DiscountPrice = 1499m,
                    StockQuantity = 100,
                    ImageUrl = "microsdCardImage.png",
                    CategoryId = existingCategories["memory cards"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2050,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "microsdCardImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("scientific calculator x1"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Scientific Calculator X1",
                    Description = "Advanced scientific calculator perfect for engineering and science students.",
                    Price = 1499m,
                    DiscountPrice = 799m,
                    StockQuantity = 100,
                    ImageUrl = "scientificCalculatorImage.png",
                    CategoryId = existingCategories["calculators"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 700,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "scientificCalculatorImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox watch active"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Watch Active",
                    Description = "Feature-packed smartwatch with heart rate monitoring and fitness tracking.",
                    Price = 6999m,
                    DiscountPrice = 2999m,
                    StockQuantity = 100,
                    ImageUrl = "smartwatchProImage.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "7 Days",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 4450,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartwatchProImage.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox watch pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Watch Pro",
                    Description = "Premium smartwatch with built-in speaker for Bluetooth calling.",
                    Price = 9999m,
                    DiscountPrice = 4999m,
                    StockQuantity = 100,
                    ImageUrl = "smartwatchProImage.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "10 Days",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartwatchProImage.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("smart outdoor camera"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Outdoor Camera",
                    Description = "Outdoor PTZ camera with color night vision and motion tracking.",
                    Price = 7999m,
                    DiscountPrice = 3499m,
                    StockQuantity = 100,
                    ImageUrl = "cctvCameraImage.png",
                    CategoryId = existingCategories["camera"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "cctvCameraImage.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("mini indoor wifi cam"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mini Indoor WiFi Cam",
                    Description = "Discreet indoor WiFi camera perfect for baby monitoring or pet watching.",
                    Price = 3999m,
                    DiscountPrice = 1999m,
                    StockQuantity = 100,
                    ImageUrl = "cctvCameraImage.png",
                    CategoryId = existingCategories["camera"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "N/A",
                    Color = "White",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1400,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "cctvCameraImage.png", ColorName = "White", ColorCode = "#fff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox latest drops edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Latest Drops Edition",
                    Description = "Be the first to experience our latest drops. This exclusive edition is strictly limited in quantity.",
                    Price = 24999m,
                    DiscountPrice = 12999m,
                    StockQuantity = 100,
                    ImageUrl = "beatboxSmartCapsuleImage.png",
                    CategoryId = existingCategories["latest drops"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "N/A",
                    Color = "Neon",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 75,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "beatboxSmartCapsuleImage.png", ColorName = "Neon", ColorCode = "#00f3ff", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("trending gear pro speaker"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Trending Gear Pro Speaker",
                    Description = "The trending gear everyone is talking about on social media. Grab yours before it sells out.",
                    Price = 15999m,
                    DiscountPrice = 8999m,
                    StockQuantity = 100,
                    ImageUrl = "partyBoom1500Image.png",
                    CategoryId = existingCategories["trending gear"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "N/A",
                    Color = "Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "partyBoom1500Image.png", ColorName = "Black", ColorCode = "#000", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox upcoming releases vip"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Upcoming Releases VIP",
                    Description = "A VIP pass giving you early access to upcoming releases and exclusive discounts.",
                    Price = 999m,
                    DiscountPrice = 499m,
                    StockQuantity = 100,
                    ImageUrl = "stoneGrenadeProImage.png",
                    CategoryId = existingCategories["upcoming releases"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "N/A",
                    Color = "Silver",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 100,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "stoneGrenadeProImage.png", ColorName = "Silver", ColorCode = "#c0c0c0", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox limited editions gold"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Limited Editions Gold",
                    Description = "Part of our exclusive limited editions line. Only 100 units made globally.",
                    Price = 49999m,
                    DiscountPrice = 29999m,
                    StockQuantity = 100,
                    ImageUrl = "soundbarEliteS9Image.png",
                    CategoryId = existingCategories["limited editions"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "N/A",
                    Color = "Gold",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 25,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "soundbarEliteS9Image.png", ColorName = "Gold", ColorCode = "#ffd700", IsPrimary = true }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox chrome ivory"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Chrome Ivory",
                    Description = "Elevate your lifestyle with the BeatBox Chrome Ivory. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5896m,
                    DiscountPrice = 1697m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 800,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma gem"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Gem",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Gem. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 3988m,
                    DiscountPrice = 1708m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1200,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox chrome iris"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Chrome Iris",
                    Description = "Elevate your lifestyle with the BeatBox Chrome Iris. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5752m,
                    DiscountPrice = 2129m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 490,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma daze"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Daze",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Daze. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8900m,
                    DiscountPrice = 3534m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2255,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar vista"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Vista",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Vista. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6210m,
                    DiscountPrice = 3779m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 660,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar discovery"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Discovery",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Discovery. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8367m,
                    DiscountPrice = 4523m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 380,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave sigma 3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Sigma 3",
                    Description = "Elevate your lifestyle with the BeatBox Wave Sigma 3. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8615m,
                    DiscountPrice = 4039m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1955,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar discovery neo"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Discovery Neo",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Discovery Neo. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10682m,
                    DiscountPrice = 4689m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 515,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave fury"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Fury",
                    Description = "Elevate your lifestyle with the BeatBox Wave Fury. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6922m,
                    DiscountPrice = 1518m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2230,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave call 2 plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Call 2 Plus",
                    Description = "Elevate your lifestyle with the BeatBox Wave Call 2 Plus. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8084m,
                    DiscountPrice = 4297m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1765,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave call"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Call",
                    Description = "Elevate your lifestyle with the BeatBox Wave Call. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 11196m,
                    DiscountPrice = 4628m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 405,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar atlas"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Atlas",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Atlas. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9013m,
                    DiscountPrice = 4076m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1880,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm infinity plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Infinity Plus",
                    Description = "Elevate your lifestyle with the BeatBox Storm Infinity Plus. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8692m,
                    DiscountPrice = 5478m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2060,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm call"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Call",
                    Description = "Elevate your lifestyle with the BeatBox Storm Call. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6413m,
                    DiscountPrice = 1561m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 120,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm call 4"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Call 4",
                    Description = "Elevate your lifestyle with the BeatBox Storm Call 4. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8285m,
                    DiscountPrice = 1604m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 595,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma orion"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Orion",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Orion. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 11949m,
                    DiscountPrice = 5340m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1525,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm call 3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Call 3",
                    Description = "Elevate your lifestyle with the BeatBox Storm Call 3. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 4967m,
                    DiscountPrice = 2347m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 145,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave call 3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Call 3",
                    Description = "Elevate your lifestyle with the BeatBox Wave Call 3. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8062m,
                    DiscountPrice = 2934m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2310,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm infinity"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Infinity",
                    Description = "Elevate your lifestyle with the BeatBox Storm Infinity. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9350m,
                    DiscountPrice = 2592m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2230,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima ember"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Ember",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Ember. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5308m,
                    DiscountPrice = 3120m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1820,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm call 3 plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Call 3 Plus",
                    Description = "Elevate your lifestyle with the BeatBox Storm Call 3 Plus. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9199m,
                    DiscountPrice = 5138m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 435,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox chrome horizon"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Chrome Horizon",
                    Description = "Elevate your lifestyle with the BeatBox Chrome Horizon. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9436m,
                    DiscountPrice = 3443m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1375,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm infinity max"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Infinity Max",
                    Description = "Elevate your lifestyle with the BeatBox Storm Infinity Max. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7180m,
                    DiscountPrice = 4180m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2215,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave sigma 3 curv"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Sigma 3 Curv",
                    Description = "Elevate your lifestyle with the BeatBox Wave Sigma 3 Curv. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9735m,
                    DiscountPrice = 3158m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 800,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar orbit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Orbit",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Orbit. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 4712m,
                    DiscountPrice = 2187m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2015,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima prime"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Prime",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Prime. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9098m,
                    DiscountPrice = 3734m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1080,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima aeris"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Aeris",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Aeris. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10412m,
                    DiscountPrice = 5159m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1435,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave astra 4"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Astra 4",
                    Description = "Elevate your lifestyle with the BeatBox Wave Astra 4. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6632m,
                    DiscountPrice = 1751m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wanderer smart"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wanderer Smart",
                    Description = "Elevate your lifestyle with the BeatBox Wanderer Smart. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5040m,
                    DiscountPrice = 1655m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1830,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox valour watch 1r"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Valour Watch 1R",
                    Description = "Elevate your lifestyle with the BeatBox Valour Watch 1R. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5266m,
                    DiscountPrice = 2438m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1680,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima regal"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Regal",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Regal. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9357m,
                    DiscountPrice = 3163m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2385,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma radiant"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Radiant",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Radiant. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8191m,
                    DiscountPrice = 3796m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 770,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox storm verge"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Storm Verge",
                    Description = "Elevate your lifestyle with the BeatBox Storm Verge. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8090m,
                    DiscountPrice = 1925m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 140,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma ascend"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Ascend",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Ascend. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7870m,
                    DiscountPrice = 5104m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 290,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox chrome eon"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Chrome Eon",
                    Description = "Elevate your lifestyle with the BeatBox Chrome Eon. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9141m,
                    DiscountPrice = 5079m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1085,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar discovery pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Discovery Pro",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Discovery Pro. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10269m,
                    DiscountPrice = 4116m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2295,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima rise"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Rise",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Rise. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6715m,
                    DiscountPrice = 2871m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1420,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox chrome endeavour"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Chrome Endeavour",
                    Description = "Elevate your lifestyle with the BeatBox Chrome Endeavour. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 8041m,
                    DiscountPrice = 4252m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1860,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar orbit 2"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Orbit 2",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Orbit 2. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9578m,
                    DiscountPrice = 2740m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1420,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar oasis"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Oasis",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Oasis. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 11616m,
                    DiscountPrice = 4713m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1340,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("valour ring 1 sizing kit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Valour Ring 1 Sizing Kit",
                    Description = "Elevate your lifestyle with the Valour Ring 1 Sizing Kit. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7801m,
                    DiscountPrice = 5108m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart-ring"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 730,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave fortune"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Fortune",
                    Description = "Elevate your lifestyle with the BeatBox Wave Fortune. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 4230m,
                    DiscountPrice = 1573m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 125,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox valour watch 1 gps"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Valour Watch 1 GPS",
                    Description = "Elevate your lifestyle with the BeatBox Valour Watch 1 GPS. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9441m,
                    DiscountPrice = 2987m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2045,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar connect ace"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Connect Ace",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Connect Ace. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9737m,
                    DiscountPrice = 2790m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1105,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima summit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Summit",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Summit. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9946m,
                    DiscountPrice = 4126m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1970,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave astra neo"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Astra Neo",
                    Description = "Elevate your lifestyle with the BeatBox Wave Astra Neo. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9084m,
                    DiscountPrice = 4182m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1290,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave aura"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Aura",
                    Description = "Elevate your lifestyle with the BeatBox Wave Aura. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7601m,
                    DiscountPrice = 1548m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1565,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave astra 3"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Astra 3",
                    Description = "Elevate your lifestyle with the BeatBox Wave Astra 3. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7336m,
                    DiscountPrice = 3355m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 80,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave spin voice"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Spin Voice",
                    Description = "Elevate your lifestyle with the BeatBox Wave Spin Voice. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5668m,
                    DiscountPrice = 3644m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1810,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar embrace"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Embrace",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Embrace. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10772m,
                    DiscountPrice = 5263m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2270,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave spectra"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Spectra",
                    Description = "Elevate your lifestyle with the BeatBox Wave Spectra. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 3607m,
                    DiscountPrice = 1595m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2075,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox ultima select"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Ultima Select",
                    Description = "Elevate your lifestyle with the BeatBox Ultima Select. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10319m,
                    DiscountPrice = 4814m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 870,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox wave magma"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Wave Magma",
                    Description = "Elevate your lifestyle with the BeatBox Wave Magma. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 4068m,
                    DiscountPrice = 1960m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 340,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma switch"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Switch",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Switch. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7957m,
                    DiscountPrice = 3737m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2330,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma z20"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Z20",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Z20. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6732m,
                    DiscountPrice = 2643m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1815,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma z40"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma Z40",
                    Description = "Elevate your lifestyle with the BeatBox Enigma Z40. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 9203m,
                    DiscountPrice = 3809m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.1,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1175,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma x400"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma X400",
                    Description = "Elevate your lifestyle with the BeatBox Enigma X400. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 10669m,
                    DiscountPrice = 3832m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 835,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox enigma x700"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Enigma X700",
                    Description = "Elevate your lifestyle with the BeatBox Enigma X700. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 6863m,
                    DiscountPrice = 2120m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 425,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar link"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Link",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Link. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 5028m,
                    DiscountPrice = 2658m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 2455,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox lunar pro lte"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Lunar Pro LTE",
                    Description = "Elevate your lifestyle with the BeatBox Lunar Pro LTE. Features include a bright HD display, advanced bluetooth calling, comprehensive health monitoring, and a premium design built to keep you connected.",
                    Price = 7697m,
                    DiscountPrice = 4796m,
                    StockQuantity = 100,
                    ImageUrl = "smartTracker.png",
                    CategoryId = existingCategories["smart watches"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 7 Days",
                    Color = "Active Black",
                    Connectivity = "Wired",
                    IsFeatured = true,
                    SoldCount = 1735,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartTracker.png", ColorName = "Cherry Blossom", ColorCode = "#ffb7c5", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 255 pro+"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 255 Pro+",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 255 Pro+. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6910m,
                    DiscountPrice = 2294m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7280,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz summit"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Summit",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Summit. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4065m,
                    DiscountPrice = 1013m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 520,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 110"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 110",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 110. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5985m,
                    DiscountPrice = 3108m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1910,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 200"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 200",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 200. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5742m,
                    DiscountPrice = 3366m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2295,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 235 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 235 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 235 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4981m,
                    DiscountPrice = 2436m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6530,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 333"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 333",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 333. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5574m,
                    DiscountPrice = 2627m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4210,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 113"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 113",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 113. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8485m,
                    DiscountPrice = 3865m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 205,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 195 v2 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 195 V2 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 195 V2 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5039m,
                    DiscountPrice = 1459m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2620,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 301 anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 301 ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 301 ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 3866m,
                    DiscountPrice = 1192m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6435,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 103 v2 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 103 V2 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 103 V2 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7259m,
                    DiscountPrice = 3376m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5620,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 261 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 261 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 261 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 2842m,
                    DiscountPrice = 1127m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5935,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 245 v2 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 245 V2 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 245 V2 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5863m,
                    DiscountPrice = 1026m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5020,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 103 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 103 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 103 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6261m,
                    DiscountPrice = 3869m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4945,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 238 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 238 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 238 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4429m,
                    DiscountPrice = 2650m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3090,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz bold"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Bold",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Bold. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7986m,
                    DiscountPrice = 3016m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7010,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 202"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 202",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 202. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4475m,
                    DiscountPrice = 1552m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4725,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz strive"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Strive",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Strive. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6459m,
                    DiscountPrice = 1610m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2775,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("rockerz 255 arc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rockerz 255 ARC",
                    Description = "Experience immersive BeatBox Signature Sound with the Rockerz 255 ARC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5348m,
                    DiscountPrice = 1778m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3820,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 210 anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 210 ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 210 ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4500m,
                    DiscountPrice = 2294m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1405,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 330"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 330",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 330. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6930m,
                    DiscountPrice = 3365m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 605,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 112"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 112",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 112. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7649m,
                    DiscountPrice = 2819m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4855,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz prime 205"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Prime 205",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Prime 205. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7512m,
                    DiscountPrice = 3785m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1495,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz trinity gen 2"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Trinity Gen 2",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Trinity Gen 2. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 9250m,
                    DiscountPrice = 3814m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6925,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 378"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 378",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 378. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8081m,
                    DiscountPrice = 3715m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5075,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz zen anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Zen ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Zen ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6366m,
                    DiscountPrice = 1330m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4365,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 245 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 245 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 245 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5854m,
                    DiscountPrice = 1158m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7395,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz trinity grande"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Trinity Grande",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Trinity Grande. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6473m,
                    DiscountPrice = 1537m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1725,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz prime 255z"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Prime 255Z",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Prime 255Z. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4943m,
                    DiscountPrice = 1051m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4880,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 203"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 203",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 203. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5919m,
                    DiscountPrice = 3132m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6310,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 150 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 150 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 150 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 9023m,
                    DiscountPrice = 3807m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7515,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana zenith pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Zenith Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Zenith Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8094m,
                    DiscountPrice = 3247m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4075,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes supreme"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Supreme",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Supreme. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5149m,
                    DiscountPrice = 2422m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7275,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 161"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 161",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 161. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4120m,
                    DiscountPrice = 1970m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5810,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 311 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 311 PRO",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 311 PRO. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5330m,
                    DiscountPrice = 1870m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1590,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes loop"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Loop",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Loop. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6253m,
                    DiscountPrice = 3001m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1835,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana ion anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Ion ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Ion ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7157m,
                    DiscountPrice = 3080m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5445,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 181 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 181 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 181 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6406m,
                    DiscountPrice = 2139m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 835,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana x tws"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana X TWS",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana X TWS. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6262m,
                    DiscountPrice = 2788m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6010,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana ion"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Ion",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Ion. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8429m,
                    DiscountPrice = 3686m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6240,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 280 anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 280 ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 280 ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4938m,
                    DiscountPrice = 2430m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3135,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 131"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 131",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 131. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6604m,
                    DiscountPrice = 1300m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2360,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes alpha"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Alpha",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Alpha. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6926m,
                    DiscountPrice = 2158m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1900,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 181 pro ss edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 181 Pro SS Edition",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 181 Pro SS Edition. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6706m,
                    DiscountPrice = 3402m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1560,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes alpha ss edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Alpha SS Edition",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Alpha SS Edition. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 3752m,
                    DiscountPrice = 2121m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6780,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 121 pro plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 121 Pro Plus",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 121 Pro Plus. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8184m,
                    DiscountPrice = 3602m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1785,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes pulse"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Pulse",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Pulse. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6642m,
                    DiscountPrice = 2903m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5580,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana crystl"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Crystl",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Crystl. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 3676m,
                    DiscountPrice = 1341m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3965,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 161 (metallic)"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 161 (Metallic)",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 161 (Metallic). Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5131m,
                    DiscountPrice = 1236m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6945,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 161 anc elite"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 161 ANC Elite",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 161 ANC Elite. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5355m,
                    DiscountPrice = 2090m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5140,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes ultra plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Ultra Plus",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Ultra Plus. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5064m,
                    DiscountPrice = 2302m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4115,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 219"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 219",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 219. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 8326m,
                    DiscountPrice = 3325m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4605,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 71"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 71",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 71. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6726m,
                    DiscountPrice = 2647m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6310,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 161 ss edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 161 SS Edition",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 161 SS Edition. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5807m,
                    DiscountPrice = 3903m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 520,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 701 anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes Prime 701 ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes Prime 701 ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7051m,
                    DiscountPrice = 1665m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5000,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 131 ss edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 131 SS Edition",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 131 SS Edition. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5114m,
                    DiscountPrice = 1302m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6135,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 148"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Airdopes 148",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Airdopes 148. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5491m,
                    DiscountPrice = 1451m,
                    StockQuantity = 100,
                    ImageUrl = "smartEarbuds.png",
                    CategoryId = existingCategories["tws earbuds"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 205,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "smartEarbuds.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 800"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 141 gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 163"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal katana blade 2.0"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Immortal katana Blade 2.0",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Immortal katana Blade 2.0. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5815m,
                    DiscountPrice = 2681m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1605,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 141"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 71 gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 111v2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes plus 311"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 131 gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 800 hidef"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 101v2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 511"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 131 pro buds"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes atom 81 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 161 pro buds"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes ace gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes ace"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvana space"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Space",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Space. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6145m,
                    DiscountPrice = 2848m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4245,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana iris"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Iris",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Iris. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6828m,
                    DiscountPrice = 3380m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4855,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana ion anc pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 512"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 412"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 131 elite anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvana crown"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Crown",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Crown. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6951m,
                    DiscountPrice = 2573m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6860,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana ivy"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Ivy",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Ivy. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5424m,
                    DiscountPrice = 1770m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6885,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes beat"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 190"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 413"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvana ivy pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Ivy Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Ivy Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6158m,
                    DiscountPrice = 2280m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7360,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox airdopes 120"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 138 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 213"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes alpha gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes atom 83"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 300"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 138 gen 2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 118 wrogn edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 161 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 91 prime"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 212"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 155"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes flex 454 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes prime 700 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes 141 elite anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes plus 318"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox airdopes ultra pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox stone 350 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 350 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 350 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4791m,
                    DiscountPrice = 1323m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5260,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone lumos"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Lumos",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Lumos. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6498m,
                    DiscountPrice = 1618m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6395,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone spinx pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Spinx Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Spinx Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6007m,
                    DiscountPrice = 3365m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6565,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 108"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 108",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 108. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5917m,
                    DiscountPrice = 3867m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 50,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 208"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 208",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 208. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6151m,
                    DiscountPrice = 3933m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1510,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 358 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 358 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 358 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7540m,
                    DiscountPrice = 2553m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4195,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone arc pro plus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Arc Pro Plus",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Arc Pro Plus. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4445m,
                    DiscountPrice = 1193m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 670,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 350"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 350",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 350. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6236m,
                    DiscountPrice = 3966m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1555,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 310"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 310",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 310. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5356m,
                    DiscountPrice = 1068m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3250,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 1200 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 1200 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 1200 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6441m,
                    DiscountPrice = 1455m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2340,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 352 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 352 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 352 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4152m,
                    DiscountPrice = 1311m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1980,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 580"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 580",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 580. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7300m,
                    DiscountPrice = 2940m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5185,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone arc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Arc",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Arc. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4678m,
                    DiscountPrice = 1272m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1975,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone arc pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Arc Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Arc Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5585m,
                    DiscountPrice = 1867m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1075,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 180"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 180",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 180. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4502m,
                    DiscountPrice = 2652m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2490,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox nirvana luxe"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Nirvana Luxe",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Nirvana Luxe. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6961m,
                    DiscountPrice = 2395m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4540,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone opus"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Opus",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Opus. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4953m,
                    DiscountPrice = 2317m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5300,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 358"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 358",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 358. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5319m,
                    DiscountPrice = 1037m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3920,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("stone 1500"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone 1500",
                    Description = "Experience immersive BeatBox Signature Sound with the Stone 1500. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6047m,
                    DiscountPrice = 2446m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.6,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 560,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 1200"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 1200",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 1200. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6678m,
                    DiscountPrice = 3570m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 475,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("stone 650r"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stone 650R",
                    Description = "Experience immersive BeatBox Signature Sound with the Stone 650R. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7508m,
                    DiscountPrice = 2091m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5605,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 193"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 193",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 193. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6662m,
                    DiscountPrice = 3662m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7315,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone vibe"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone Vibe",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone Vibe. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6113m,
                    DiscountPrice = 2461m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 580,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox stone 350 pro naruto edition"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Stone 350 Pro Naruto Edition",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Stone 350 Pro Naruto Edition. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6781m,
                    DiscountPrice = 3313m,
                    StockQuantity = 100,
                    ImageUrl = "portableSpeakerHero.png",
                    CategoryId = existingCategories["portable speakers"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "portableSpeakerHero.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 412"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 412",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 412. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6744m,
                    DiscountPrice = 2284m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 6285,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 413"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 413",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 413. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5495m,
                    DiscountPrice = 1194m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.3,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 1600,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 650 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 650 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 650 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6798m,
                    DiscountPrice = 3225m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3035,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 480"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 480",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 480. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7805m,
                    DiscountPrice = 2386m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.4,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 2545,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 512 anc"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 512 ANC",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 512 ANC. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 4478m,
                    DiscountPrice = 1086m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.7,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 3340,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz plus 550"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz Plus 550",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz Plus 550. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 6591m,
                    DiscountPrice = 3488m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.2,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5845,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 430"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 430",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 430. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5346m,
                    DiscountPrice = 3722m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 5130,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 421"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 421",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 421. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 5341m,
                    DiscountPrice = 3581m,
                    StockQuantity = 100,
                    ImageUrl = "heroHeadphones.png",
                    CategoryId = existingCategories["wireless headphones"].Id,
                    Brand = "BeatBox",
                    Rating = 4.9,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 410,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "heroHeadphones.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 425"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 411"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz plus 450 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvana 751 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz plus 550 anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox bassheads 900"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz prime 415"))
            {
                
            }

            if (!existingProductNames.Contains("bassheads 901 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 551 anc pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz trendz"))
            {
                
            }

            if (!existingProductNames.Contains("bassheads 900 c pro"))
            {
                
            }

            if (!existingProductNames.Contains("bassheads 900 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 371"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 371",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 371. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 3117m,
                    DiscountPrice = 1319m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 5,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 4895,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 460 naruto edition"))
            {
                
            }

            if (!existingProductNames.Contains("nirvana eutopia - dhruv kapoor edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 370 pro"))
            {
                productsToAdd.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "BeatBox Rockerz 370 Pro",
                    Description = "Experience immersive BeatBox Signature Sound with the BeatBox Rockerz 370 Pro. Engineered for power, clarity, and deep bass to elevate your listening experience.",
                    Price = 7529m,
                    DiscountPrice = 2669m,
                    StockQuantity = 100,
                    ImageUrl = "wirelessNeckband.png",
                    CategoryId = existingCategories["wireless neckbands"].Id,
                    Brand = "BeatBox",
                    Rating = 4.8,
                    BatteryLife = "Up to 40 Hours",
                    Color = "Active Black",
                    Connectivity = "Bluetooth v5.3",
                    IsFeatured = true,
                    SoldCount = 7195,
                    DeliveryDays = 3,
                    Images = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Active Black", ColorCode = "#000000", IsPrimary = true },
                        new ProductImage { ImageUrl = "wirelessNeckband.png", ColorName = "Bold Blue", ColorCode = "#0000ff", IsPrimary = false }
                    },
                    Faqs = new List<ProductFaq>
                    {
                        
                    }
                });
            }

            if (!existingProductNames.Contains("beatbox rockerz 400 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvana eutopia"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockid rush"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 551anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 558 sunburn edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 650 sunburn edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 650 dc edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450r"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 300"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 460"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 wonder woman dc edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 batman dc edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 superman dc edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 dc edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 400"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 700"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox bassheads 950v2"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 iron man"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 captain america marvel edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 black panther marvel edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 1300"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 660"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 200"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox immortal 1000d"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 450 pro"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 650"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox nirvanaa 1007anc"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 600 kkr edition"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 560"))
            {
                
            }

            if (!existingProductNames.Contains("beatbox rockerz 550"))
            {
                
            }

            if (productsToAdd.Any())
            {
                await context.Products.AddRangeAsync(productsToAdd);
                await context.SaveChangesAsync();
                
                // Add Inventory for them
                foreach(var p in productsToAdd) {
                    var inv = new Inventory 
                    { 
                        Id = Guid.NewGuid(), 
                        ProductId = p.Id, 
                        AvailableStock = 100, 
                        ReservedStock = 0, 
                        WarehouseLocation = "Main", 
                        LastUpdated = DateTime.UtcNow 
                    };
                    context.Inventories.Add(inv);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
