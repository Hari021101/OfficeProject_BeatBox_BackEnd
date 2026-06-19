using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BeatBox.ImageDownloader
{
    public class Program
    {
        private class CategoryConfig
        {
            public string FolderName { get; }
            public string SingularPrefix { get; }
            public string SearchTerm { get; }

            public CategoryConfig(string folderName, string singularPrefix, string searchTerm)
            {
                FolderName = folderName;
                SingularPrefix = singularPrefix;
                SearchTerm = searchTerm;
            }
        }

        private static readonly List<CategoryConfig> Categories = new()
        {
            new("soundbars", "soundbar", "soundbar speaker"),
            new("party-speakers", "party-speaker", "party speaker"),
            new("portable-speakers", "portable-speaker", "portable bluetooth speaker"),
            new("tws", "tws", "wireless earbuds"),
            new("neckbands", "neckband", "neckband earphones"),
            new("wireless-headphones", "wireless-headphone", "wireless headphones"),
            new("wired-earphones", "wired-earphone", "wired earphones"),
            new("usb-speakers", "usb-speaker", "computer speakers"),
            new("conference-speakers", "conference-speaker", "conference speakerphone"),
            new("wireless-microphones", "wireless-microphone", "wireless microphone"),
            new("power-bank", "power-bank", "power bank portable charger"),
            new("cables", "cable", "usb c charging cable"),
            new("chargers", "charger", "usb wall charger plug"),
            new("wireless-charger", "wireless-charger", "wireless charging pad"),
            new("mobile-holder", "mobile-holder", "car phone mount holder"),
            new("keyboards", "keyboard", "computer keyboard"),
            new("mice", "mouse", "computer mouse"),
            new("gaming-keyboards", "gaming-keyboard", "rgb gaming keyboard"),
            new("laptop-bags", "laptop-bag", "laptop backpack"),
            new("projectors", "projector", "video projector"),
            new("car-charger", "car-charger", "usb car charger adapter"),
            new("car-bluetooth", "car-bluetooth", "car bluetooth fm transmitter"),
            new("tyre-inflators", "tyre-inflator", "portable tire inflator car air pump"),
            new("ear-cleaners", "ear-cleaner", "ear cleaner cleaning kit"),
            new("portable-fans", "portable-fan", "handheld portable fan"),
            new("selfie-stick", "selfie-stick", "selfie stick tripod"),
            new("flashlight", "flashlight", "led flashlight torch"),
            new("stylus", "stylus", "stylus pen tablet"),
            new("electric-kettle", "electric-kettle", "electric kettle water boiler"),
            new("hair-dryer", "hair-dryer", "hair dryer blow dryer"),
            new("humidifiers", "humidifier", "ultrasonic humidifier diffuser"),
            new("massagers", "massager", "back neck massage gun massager"),
            new("rechargeable-battery", "rechargeable-battery", "rechargeable aa aaa batteries"),
            new("default", "default", "electronic gadget tech product")
        };

        private const string MetadataFileName = "downloaded_images.json";

        public static async Task Main(string[] args)
        {
            // 1. Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // 2. Set up logging
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("================================================");
            logger.LogInformation("  BeatBox Product Image Downloader Utility       ");
            logger.LogInformation("================================================");

            // 3. Resolve Pexels API Key
            string? apiKey = GetApiKey(args, configuration, logger);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                logger.LogError("Pexels API Key is missing. Please set the PEXELS_API_KEY environment variable, specify 'Pexels:ApiKey' in appsettings.json, or pass it via '--key <your-key>' arguments.");
                return;
            }

            // 4. Resolve Target Directory (wwwroot/images/products)
            string rootPath = ResolveRootDirectory();
            string targetProductsPath = Path.Combine(rootPath, "API", "wwwroot", "images", "products");
            logger.LogInformation($"Target products directory: {targetProductsPath}");

            if (!Directory.Exists(targetProductsPath))
            {
                logger.LogInformation($"Creating products root directory: {targetProductsPath}");
                Directory.CreateDirectory(targetProductsPath);
            }

            // 5. Load Downloaded Metadata (for deduplication across runs)
            var metadataFilePath = Path.Combine(targetProductsPath, MetadataFileName);
            var downloadedIds = LoadDownloadedMetadata(metadataFilePath, logger);

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("BeatBoxImageDownloader/1.0");

            int totalDownloaded = 0;
            int totalSkipped = 0;

            // 6. Process each category
            foreach (var category in Categories)
            {
                logger.LogInformation("");
                logger.LogInformation($"------------------------------------------------");
                logger.LogInformation($"Processing Category: '{category.FolderName}'");
                logger.LogInformation($"------------------------------------------------");

                var categoryDir = Path.Combine(targetProductsPath, category.FolderName);
                if (!Directory.Exists(categoryDir))
                {
                    logger.LogInformation($"Creating directory: {categoryDir}");
                    Directory.CreateDirectory(categoryDir);
                }

                // Check how many images are already in the folder
                var existingFiles = Directory.GetFiles(categoryDir, "*.jpg")
                    .Select(Path.GetFileName)
                    .Where(name => name != null && name.StartsWith(category.SingularPrefix, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (existingFiles.Count >= 15)
                {
                    logger.LogInformation($"Category '{category.FolderName}' already has {existingFiles.Count} images. Skipping download.");
                    continue;
                }

                int imagesNeeded = 15 - existingFiles.Count;
                logger.LogInformation($"Need to download {imagesNeeded} images for '{category.FolderName}'...");

                int downloadedForCategory = 0;
                int page = 1;
                const int perPage = 40;

                while (downloadedForCategory < imagesNeeded)
                {
                    var searchUrl = $"https://api.pexels.com/v1/search?query={Uri.EscapeDataString(category.SearchTerm)}&per_page={perPage}&page={page}";
                    logger.LogInformation($"Fetching page {page} of Pexels search for '{category.SearchTerm}'...");

                    PexelsSearchResponse? searchResponse = null;
                    try
                    {
                        searchResponse = await httpClient.GetFromJsonAsync<PexelsSearchResponse>(searchUrl);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Failed to search Pexels for '{category.SearchTerm}'.");
                        break;
                    }

                    if (searchResponse == null || searchResponse.Photos.Count == 0)
                    {
                        logger.LogWarning($"No photos returned for '{category.SearchTerm}' on page {page}. Stopping search for this category.");
                        break;
                    }

                    logger.LogInformation($"Found {searchResponse.Photos.Count} photo candidates.");

                    foreach (var photo in searchResponse.Photos)
                    {
                        if (downloadedForCategory >= imagesNeeded) break;

                        // Skip duplicate image IDs
                        if (downloadedIds.Contains(photo.Id))
                        {
                            logger.LogDebug($"Skipping photo {photo.Id} (already downloaded).");
                            totalSkipped++;
                            continue;
                        }

                        // Determine best URL to download
                        // We prefer large2x or original.
                        string downloadUrl = !string.IsNullOrEmpty(photo.Src.Large2x) ? photo.Src.Large2x : photo.Src.Original;
                        if (string.IsNullOrEmpty(downloadUrl))
                        {
                            continue;
                        }

                        logger.LogInformation($"[{downloadedForCategory + 1}/{imagesNeeded}] Downloading photo {photo.Id}...");

                        try
                        {
                            // Download image bytes
                            byte[] imageBytes = await httpClient.GetByteArrayAsync(downloadUrl);

                            // Load and resize using ImageSharp
                            using var image = Image.Load(imageBytes);

                            if (image.Width > 1200)
                            {
                                var oldWidth = image.Width;
                                var oldHeight = image.Height;
                                var newHeight = (int)((double)image.Height * 1200 / image.Width);
                                image.Mutate(x => x.Resize(1200, newHeight));
                                logger.LogInformation($"Resized image {photo.Id} from {oldWidth}x{oldHeight} to 1200x{newHeight}");
                            }

                            // Find next available index in the folder
                            int fileIndex = 1;
                            string targetFile;
                            do
                            {
                                targetFile = Path.Combine(categoryDir, $"{category.SingularPrefix}{fileIndex}.jpg");
                                fileIndex++;
                            } while (File.Exists(targetFile));

                            // Save as Jpeg
                            await image.SaveAsJpegAsync(targetFile);
                            logger.LogInformation($"Saved to {Path.GetRelativePath(rootPath, targetFile)}");

                            // Add to metadata
                            downloadedIds.Add(photo.Id);
                            downloadedForCategory++;
                            totalDownloaded++;

                            // Save metadata incrementally
                            SaveDownloadedMetadata(metadataFilePath, downloadedIds, logger);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Error downloading or processing photo {photo.Id}.");
                        }
                    }

                    page++;
                    // If we have parsed a few pages and still cannot satisfy the requirement, stop to avoid infinite loop
                    if (page > 5)
                    {
                        logger.LogWarning($"Reached page limit (5) for search query '{category.SearchTerm}'. Stopping search.");
                        break;
                    }
                }

                logger.LogInformation($"Completed category '{category.FolderName}'. Downloaded {downloadedForCategory} images.");
            }

            logger.LogInformation("");
            logger.LogInformation("================================================");
            logger.LogInformation($"Process completed successfully!");
            logger.LogInformation($"Total Images Downloaded: {totalDownloaded}");
            logger.LogInformation($"Duplicate Images Skipped: {totalSkipped}");
            logger.LogInformation("================================================");
        }

        private static string? GetApiKey(string[] args, IConfiguration configuration, ILogger logger)
        {
            // 1. CommandLine arguments
            for (int i = 0; i < args.Length; i++)
            {
                if ((args[i] == "--key" || args[i] == "--api-key" || args[i] == "-k") && i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }

            // 2. Environment Variable
            string? key = Environment.GetEnvironmentVariable("PEXELS_API_KEY");
            if (!string.IsNullOrWhiteSpace(key))
            {
                return key;
            }

            // 3. Configuration file (appsettings.json)
            key = configuration["Pexels:ApiKey"];
            if (!string.IsNullOrWhiteSpace(key))
            {
                return key;
            }

            // 4. Interactive prompt
            logger.LogInformation("Please enter your Pexels API Key: ");
            return Console.ReadLine()?.Trim();
        }

        private static string ResolveRootDirectory()
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            while (!string.IsNullOrEmpty(currentDir))
            {
                if (File.Exists(Path.Combine(currentDir, "EcommerceSolution.slnx")) || 
                    Directory.Exists(Path.Combine(currentDir, "API")))
                {
                    return currentDir;
                }
                currentDir = Directory.GetParent(currentDir)?.FullName;
            }
            return Directory.GetCurrentDirectory(); // Fallback
        }

        private static HashSet<long> LoadDownloadedMetadata(string path, ILogger logger)
        {
            var ids = new HashSet<long>();
            if (!File.Exists(path))
            {
                return ids;
            }

            try
            {
                var content = File.ReadAllText(path);
                var metadata = JsonSerializer.Deserialize<DownloadedMetadata>(content);
                if (metadata?.DownloadedIds != null)
                {
                    foreach (var id in metadata.DownloadedIds)
                    {
                        ids.Add(id);
                    }
                }
                logger.LogInformation($"Loaded {ids.Count} previously downloaded photo IDs from {MetadataFileName}");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Failed to load metadata file: {path}. Starting with clean list.");
            }

            return ids;
        }

        private static void SaveDownloadedMetadata(string path, HashSet<long> ids, ILogger logger)
        {
            try
            {
                var metadata = new DownloadedMetadata { DownloadedIds = ids.ToList() };
                var json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Failed to save metadata file: {path}");
            }
        }

        public class DownloadedMetadata
        {
            [JsonPropertyName("downloadedIds")]
            public List<long> DownloadedIds { get; set; } = new();
        }

        public class PexelsSearchResponse
        {
            [JsonPropertyName("photos")]
            public List<PexelsPhoto> Photos { get; set; } = new();
        }

        public class PexelsPhoto
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }

            [JsonPropertyName("height")]
            public int Height { get; set; }

            [JsonPropertyName("src")]
            public PexelsPhotoSrc Src { get; set; } = new();
        }

        public class PexelsPhotoSrc
        {
            [JsonPropertyName("original")]
            public string Original { get; set; } = "";

            [JsonPropertyName("large2x")]
            public string Large2x { get; set; } = "";

            [JsonPropertyName("large")]
            public string Large { get; set; } = "";
        }
    }
}
