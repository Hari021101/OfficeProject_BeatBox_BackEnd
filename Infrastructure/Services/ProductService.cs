using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly IFileUploadService _fileUploadService;
    private readonly IBusinessEventPublisher _eventPublisher;
    private readonly IMemoryCache _cache;

    public ProductService(
        IProductRepository productRepository, 
        IMapper mapper, 
        AppDbContext context, 
        IFileUploadService fileUploadService,
        IBusinessEventPublisher eventPublisher,
        IMemoryCache cache)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
        _fileUploadService = fileUploadService;
        _eventPublisher = eventPublisher;
        _cache = cache;
    }

    private void ClearProductCache(Guid productId)
    {
        _cache.Remove("products_all");
        _cache.Remove($"product_{productId}");
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        await _productRepository.AddAsync(product);
        
        var createdProduct = await _productRepository.GetByIdAsync(product.Id);
        var mapped = _mapper.Map<ProductResponseDto>(createdProduct ?? product);

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "CREATED",
            EntityType = "Product",
            EntityId = mapped.Id.ToString(),
            Title = mapped.Name,
            Description = $"Product '{mapped.Name}' created by Administrator",
            Icon = "PlusCircle",
            ColorClass = "text-success",
            BgClass = "bg-success"
        });

        return mapped;
    }

    public async Task UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null) return;

        _mapper.Map(productUpdateDto, existingProduct);
        await _productRepository.UpdateAsync(existingProduct);
        ClearProductCache(id);

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "Product",
            EntityId = existingProduct.Id.ToString(),
            Title = existingProduct.Name,
            Description = $"Product '{existingProduct.Name}' details updated by Administrator",
            Icon = "Edit",
            ColorClass = "text-info",
            BgClass = "bg-info"
        });
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct != null)
        {
            await _productRepository.DeleteAsync(id);
            ClearProductCache(id);

            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = "DELETED",
                EntityType = "Product",
                EntityId = id.ToString(),
                Title = existingProduct.Name,
                Description = $"Product '{existingProduct.Name}' deleted by Administrator",
                Icon = "Trash2",
                ColorClass = "text-danger",
                BgClass = "bg-danger"
            });
        }
    }

    public async Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await _productRepository.SearchAsync(searchTerm);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<IEnumerable<ProductResponseDto>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color)
    {
        var products = await _productRepository.FilterAsync(minPrice, maxPrice, brand, color);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetPagedAsync(pageNumber, pageSize);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
    public async Task AddReviewAsync(Guid productId, string userId, AddReviewDto dto)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null)
            throw new Exception("Product not found");

        var review = new ProductReview
        {
            ProductId = productId,
            UserId = userId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedDate = DateTime.UtcNow,
            IsVerifiedPurchase = true
        };

        product.Reviews ??= new List<ProductReview>();

        product.Reviews.Add(review);

        await _productRepository.UpdateAsync(product);
    }

    public async Task BulkDeleteAsync(IEnumerable<Guid> productIds)
    {
        foreach (var id in productIds)
        {
            await _productRepository.DeleteAsync(id);
            ClearProductCache(id);
        }
    }

    public async Task BulkUpdateFeaturedAsync(IEnumerable<Guid> productIds, bool isFeatured)
    {
        foreach (var id in productIds)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.IsFeatured = isFeatured;
                await _productRepository.UpdateAsync(product);
                ClearProductCache(id);
            }
        }
    }

    public async Task<ProductVariantDto> AddVariantAsync(Guid productId, ProductVariantCreateDto dto)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new Exception("Product not found");

        var variant = _mapper.Map<ProductVariant>(dto);
        variant.ProductId = productId;
        variant.Id = Guid.NewGuid();

        await _context.ProductVariants.AddAsync(variant);
        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(dto.ImageUrl))
        {
            var image = new ProductVariantImage
            {
                Id = Guid.NewGuid(),
                VariantId = variant.Id,
                ImageUrl = dto.ImageUrl,
                IsPrimary = true,
                DisplayOrder = 1
            };
            await _context.ProductVariantImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        var createdVariant = await _context.ProductVariants
            .Include(v => v.Images)
            .FirstOrDefaultAsync(v => v.Id == variant.Id);

        ClearProductCache(productId);

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "CREATED",
            EntityType = "Variant",
            EntityId = variant.Id.ToString(),
            Title = $"{product?.Name ?? "Product"} - {variant.Color}",
            Description = $"Variant '{variant.Color}' created with SKU: {variant.Sku}",
            Icon = "PlusCircle",
            ColorClass = "text-success",
            BgClass = "bg-success"
        });

        return _mapper.Map<ProductVariantDto>(createdVariant);
    }

    public async Task<ProductVariantDto> UpdateVariantAsync(Guid variantId, ProductVariantUpdateDto dto)
    {
        var variant = await _context.ProductVariants
            .Include(v => v.Images)
            .FirstOrDefaultAsync(v => v.Id == variantId);
        if (variant == null) throw new Exception("Variant not found");

        _mapper.Map(dto, variant);
        await _context.SaveChangesAsync();
        ClearProductCache(variant.ProductId);

        var product = await _context.Products.FindAsync(variant.ProductId);
        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "Variant",
            EntityId = variant.Id.ToString(),
            Title = $"{product?.Name ?? "Product"} - {variant.Color}",
            Description = $"Variant '{variant.Color}' updated by Administrator (SKU: {variant.Sku}, Price: ₹{variant.Price}, Stock: {variant.StockQuantity})",
            Icon = "Edit",
            ColorClass = "text-info",
            BgClass = "bg-info"
        });

        return _mapper.Map<ProductVariantDto>(variant);
    }

    public async Task DeleteVariantAsync(Guid variantId)
    {
        var variant = await _context.ProductVariants
            .Include(v => v.Images)
            .FirstOrDefaultAsync(v => v.Id == variantId);
        if (variant != null)
        {
            var product = await _context.Products.FindAsync(variant.ProductId);
            var color = variant.Color;
            var productId = variant.ProductId;

            if (variant.Images != null && variant.Images.Any())
            {
                _context.ProductVariantImages.RemoveRange(variant.Images);
            }

            _context.ProductVariants.Remove(variant);
            await _context.SaveChangesAsync();
            ClearProductCache(productId);

            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = "DELETED",
                EntityType = "Variant",
                EntityId = variantId.ToString(),
                Title = $"{product?.Name ?? "Product"} - {color}",
                Description = $"Variant '{color}' deleted by Administrator",
                Icon = "Trash2",
                ColorClass = "text-danger",
                BgClass = "bg-danger"
            });
        }
    }

    public async Task<List<ProductVariantImageDto>> UploadVariantImagesAsync(Guid variantId, List<(Stream Stream, string FileName, string ContentType, long Length)> files)
    {
        var variant = await _context.ProductVariants.FindAsync(variantId);
        if (variant == null) throw new Exception("Variant not found");

        var currentImages = await _context.ProductVariantImages
            .Where(i => i.VariantId == variantId)
            .ToListAsync();
        int maxOrder = currentImages.Any() ? currentImages.Max(i => i.DisplayOrder) : 0;
        bool hasPrimary = currentImages.Any(i => i.IsPrimary);

        var newImages = new List<ProductVariantImage>();

        foreach (var file in files)
        {
            var relativePath = await _fileUploadService.UploadProductImageAsync(
                file.Stream,
                file.FileName,
                file.ContentType,
                file.Length
            );

            maxOrder++;
            var image = new ProductVariantImage
            {
                Id = Guid.NewGuid(),
                VariantId = variantId,
                ImageUrl = relativePath,
                IsPrimary = !hasPrimary && newImages.Count == 0,
                DisplayOrder = maxOrder
            };

            newImages.Add(image);
            await _context.ProductVariantImages.AddAsync(image);
        }

        await _context.SaveChangesAsync();
        ClearProductCache(variant.ProductId);

        var product = await _context.Products.FindAsync(variant.ProductId);
        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "Variant",
            EntityId = variantId.ToString(),
            Title = $"{product?.Name ?? "Product"} - {variant.Color}",
            Description = $"Uploaded {files.Count} new gallery images for variant '{variant.Color}'",
            Icon = "Upload",
            ColorClass = "text-info",
            BgClass = "bg-info"
        });

        return _mapper.Map<List<ProductVariantImageDto>>(newImages);
    }

    public async Task DeleteImageAsync(Guid imageId)
    {
        var image = await _context.ProductVariantImages.FindAsync(imageId);
        if (image != null)
        {
            bool wasPrimary = image.IsPrimary;
            var variantId = image.VariantId;

            _context.ProductVariantImages.Remove(image);
            await _context.SaveChangesAsync();

            if (wasPrimary)
            {
                var remainingImage = await _context.ProductVariantImages
                    .Where(i => i.VariantId == variantId)
                    .OrderBy(i => i.DisplayOrder)
                    .FirstOrDefaultAsync();

                if (remainingImage != null)
                {
                    remainingImage.IsPrimary = true;
                    await _context.SaveChangesAsync();
                }
            }

            var variant = await _context.ProductVariants.FindAsync(variantId);
            var product = variant != null ? await _context.Products.FindAsync(variant.ProductId) : null;
            if (variant != null)
            {
                ClearProductCache(variant.ProductId);
            }

            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = "UPDATED",
                EntityType = "Variant",
                EntityId = variantId.ToString(),
                Title = $"{product?.Name ?? "Product"} - {variant?.Color}",
                Description = $"Removed a gallery image from variant '{variant?.Color}'",
                Icon = "Trash2",
                ColorClass = "text-info",
                BgClass = "bg-info"
            });
        }
    }

    public async Task ReorderImagesAsync(List<ImageOrderDto> imageOrders)
    {
        Guid? firstVariantId = null;
        foreach (var order in imageOrders)
        {
            var image = await _context.ProductVariantImages.FindAsync(order.ImageId);
            if (image != null)
            {
                image.DisplayOrder = order.DisplayOrder;
                if (firstVariantId == null) firstVariantId = image.VariantId;
            }
        }
        await _context.SaveChangesAsync();

        if (firstVariantId != null)
        {
            var variant = await _context.ProductVariants.FindAsync(firstVariantId.Value);
            if (variant != null)
            {
                ClearProductCache(variant.ProductId);
            }
        }
    }

    public async Task SetPrimaryImageAsync(Guid imageId)
    {
        var image = await _context.ProductVariantImages.FindAsync(imageId);
        if (image == null) throw new Exception("Image not found");

        var otherImages = await _context.ProductVariantImages
            .Where(i => i.VariantId == image.VariantId)
            .ToListAsync();

        foreach (var img in otherImages)
        {
            img.IsPrimary = (img.Id == imageId);
        }

        await _context.SaveChangesAsync();

        var variant = await _context.ProductVariants.FindAsync(image.VariantId);
        if (variant != null)
        {
            ClearProductCache(variant.ProductId);
        }
        var product = variant != null ? await _context.Products.FindAsync(variant.ProductId) : null;
        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "Variant",
            EntityId = image.VariantId.ToString(),
            Title = $"{product?.Name ?? "Product"} - {variant?.Color}",
            Description = $"Primary image updated for variant '{variant?.Color}'",
            Icon = "Star",
            ColorClass = "text-warning",
            BgClass = "bg-warning"
        });
    }
}