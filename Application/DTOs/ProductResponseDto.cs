namespace Application.DTOs;

public class ProductResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public string BatteryLife { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string Connectivity { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
    public int SoldCount { get; set; }

    public int DeliveryDays { get; set; }

    public double AverageRating { get; set; }

    public int ReviewCount { get; set; }

    public List<ProductReviewDto> Reviews { get; set; } = new();

    public List<ProductImageDto> Images { get; set; } = new();

    public List<ProductFaqDto> Faqs { get; set; } = new();

    private System.Collections.Generic.Dictionary<string, string>? _specs;
    public System.Collections.Generic.Dictionary<string, string> Specs
    {
        get
        {
            if (_specs != null) return _specs;

            var specs = new System.Collections.Generic.Dictionary<string, string>();
            var cat = (CategoryName ?? string.Empty).ToLower().Trim();

            // Default Specs for all products
            specs["Brand"] = string.IsNullOrEmpty(Brand) ? "BeatBox" : Brand;
            if (!string.IsNullOrEmpty(Color)) specs["Color"] = Color;
            if (!string.IsNullOrEmpty(Connectivity)) specs["Connectivity"] = Connectivity;
            if (!string.IsNullOrEmpty(BatteryLife)) specs["Battery Life"] = BatteryLife;

            // Category Specific Specs
            if (cat.Contains("earbud") || cat.Contains("tws"))
            {
                specs["Device Type"] = "Wireless Earbuds (TWS)";
                specs["Bluetooth Version"] = "v5.3";
                specs["Driver Size"] = "10mm Dynamic Drivers";
                specs["Water Resistance"] = "IPX5 Sweat & Water Resistant";
                specs["Charging Port"] = "Type-C Fast Charging";
                specs["Noise Cancellation"] = "ENx Technology for Clear Calls";
                if (!specs.ContainsKey("Battery Life") || string.IsNullOrEmpty(specs["Battery Life"]))
                {
                    specs["Battery Life"] = "Up to 40 Hours";
                }
                specs["Charging Time"] = "1.5 Hours";
                specs["Warranty"] = "1 Year Brand Warranty";
            }
            else if (cat.Contains("headphone"))
            {
                specs["Device Type"] = "Wireless Over-Ear Headphones";
                specs["Bluetooth Version"] = "v5.2";
                specs["Driver Size"] = "40mm Drivers";
                specs["Water Resistance"] = "IPX4 Sweat Resistant";
                specs["Charging Port"] = "Type-C Fast Charging";
                specs["Audio Jack"] = "3.5mm Aux Input Supported";
                if (!specs.ContainsKey("Battery Life") || string.IsNullOrEmpty(specs["Battery Life"]))
                {
                    specs["Battery Life"] = "Up to 60 Hours";
                }
                specs["Charging Time"] = "2 Hours";
                specs["Warranty"] = "1 Year Brand Warranty";
            }
            else if (cat.Contains("neckband"))
            {
                specs["Device Type"] = "Wireless Neckband Earphones";
                specs["Bluetooth Version"] = "v5.3";
                specs["Driver Size"] = "12mm Dynamic Drivers";
                specs["Water Resistance"] = "IPX5 Sweat & Water Resistant";
                specs["Charging Port"] = "Type-C Fast Charging";
                specs["Fast Charge"] = "10 mins charge = 10 hours playtime";
                if (!specs.ContainsKey("Battery Life") || string.IsNullOrEmpty(specs["Battery Life"]))
                {
                    specs["Battery Life"] = "Up to 30 Hours";
                }
                specs["Charging Time"] = "1 Hour";
                specs["Warranty"] = "1 Year Brand Warranty";
            }
            else if (cat.Contains("watch"))
            {
                specs["Device Type"] = "Smart Wearable / Smartwatch";
                specs["Display"] = "1.78\" AMOLED Display";
                specs["Resolution"] = "368 * 448 Pixels";
                specs["Bluetooth Calling"] = "Supported (Built-in Mic & Speaker)";
                specs["Water Resistance"] = "IP68 Dust & Water Resistant";
                specs["Health Trackers"] = "Heart Rate, SpO2, Sleep, Stress & Steps Monitor";
                specs["Sports Modes"] = "100+ Active Sports Modes";
                if (!specs.ContainsKey("Battery Life") || string.IsNullOrEmpty(specs["Battery Life"]))
                {
                    specs["Battery Life"] = "Up to 7 Days";
                }
                specs["Charging Time"] = "2 Hours";
                specs["Warranty"] = "1 Year Brand Warranty";
            }
            else if (cat.Contains("speaker") || cat.Contains("soundbar"))
            {
                specs["Device Type"] = "Portable Bluetooth Speaker";
                specs["Bluetooth Version"] = "v5.0";
                specs["Driver Output"] = cat.Contains("soundbar") ? "120W RMS Signature Sound" : "16W RMS Sound";
                specs["Connectivity Modes"] = "Bluetooth, AUX, USB & TF Card";
                specs["Water Resistance"] = cat.Contains("soundbar") ? "Not Applicable" : "IPX7 Waterproof";
                if (!specs.ContainsKey("Battery Life") || string.IsNullOrEmpty(specs["Battery Life"]))
                {
                    specs["Battery Life"] = cat.Contains("soundbar") ? "AC Powered" : "Up to 12 Hours";
                }
                specs["Charging Time"] = cat.Contains("soundbar") ? "N/A" : "3 Hours";
                specs["Warranty"] = "1 Year Brand Warranty";
            }
            else
            {
                // Generic specs for other categories (Cables, Chargers, etc.)
                specs["Device Type"] = CategoryName ?? "Mobile Accessory";
                specs["Material"] = "Premium Durable Build";
                specs["Input / Output"] = "High Speed Transfer / Fast Charging Supported";
                specs["Warranty"] = "1 Year Brand Warranty";
            }

            _specs = specs;
            return _specs;
        }
        set { _specs = value; }
    }
}