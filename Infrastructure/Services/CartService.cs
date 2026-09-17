using Application.Common.Options;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly OrderSettingsOptions _orderSettingsOptions;

    public CartService(
        ICartRepository cartRepository, 
        IMapper mapper,
        IProductRepository productRepository,
        IOptions<OrderSettingsOptions> orderSettingsOptions)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _orderSettingsOptions = orderSettingsOptions.Value;
    }

    public async Task<CartDto> GetCartAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task AddToCartAsync(string userId, CartAddDto cartAddDto)
    {
        if (cartAddDto.Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0.");
        }

        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        // Create cart if not exists
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _cartRepository.AddCartAsync(cart);
            await _cartRepository.SaveChangesAsync();
        }

        // Fetch product from database
        var product = await _productRepository.GetByIdAsync(cartAddDto.ProductId);

        if (product == null)
            throw new KeyNotFoundException("Product not found");

        // If client didn't provide a variant id, pick the first available variant.
        ProductVariant? variant = null;
        if (cartAddDto.VariantId == Guid.Empty)
        {
            variant = product.Variants.FirstOrDefault();
        }
        else
        {
            variant = product.Variants.FirstOrDefault(v => v.Id == cartAddDto.VariantId);
        }

        if (variant == null)
            throw new KeyNotFoundException($"Variant not found for product {product.Id}. Provide a valid VariantId.");

        var maxQty = _orderSettingsOptions.MaxNormalOrderQuantity;

        // Calculate existing quantity for the same variant in user's cart
        var existingVariantQuantity = cart.CartItems
            .Where(ci => ci.VariantId == variant.Id)
            .Sum(ci => ci.Quantity);

        var proposedQuantity = existingVariantQuantity + cartAddDto.Quantity;

        if (proposedQuantity > maxQty)
        {
            throw new InvalidOperationException($"Maximum quantity per product is {maxQty}. For larger quantities, please use Corporate & Bulk Orders.");
        }

        if (variant.StockQuantity < proposedQuantity)
        {
            throw new InvalidOperationException($"Only {variant.StockQuantity} units are available in stock.");
        }

        // Validate customization fields if personalization is requested
        bool isPersonalised = false;
        string? engravingName = null;
        string? engravingDate = null;
        string? engravingMessage = null;
        decimal engravingPrice = 0.00m;

        if (cartAddDto.IsPersonalised)
        {
            if (!product.IsEngravingAvailable)
            {
                throw new InvalidOperationException("Custom engraving is not available for this product.");
            }

            if (string.IsNullOrWhiteSpace(cartAddDto.EngravingName))
            {
                throw new InvalidOperationException("Engraving name is required for personalized products.");
            }

            if (cartAddDto.EngravingName.Length > 12)
            {
                throw new InvalidOperationException("Engraving name cannot exceed 12 characters.");
            }

            if (!string.IsNullOrEmpty(cartAddDto.EngravingMessage) && cartAddDto.EngravingMessage.Length > 40)
            {
                throw new InvalidOperationException("Engraving message cannot exceed 40 characters.");
            }

            isPersonalised = true;
            engravingName = cartAddDto.EngravingName.ToUpper();
            engravingDate = cartAddDto.EngravingDate;
            engravingMessage = cartAddDto.EngravingMessage?.ToUpper();
            engravingPrice = product.EngravingPrice;
        }

        // Create cart item
        var cartItem = new CartItem
        {
            CartId = cart.CartId,
            ProductId = cartAddDto.ProductId,
            VariantId = variant.Id,
            ProductVariantId = variant.Id,
            Quantity = cartAddDto.Quantity,
            UnitPrice = variant.DiscountPrice.HasValue && variant.DiscountPrice.Value > 0
                    ? variant.DiscountPrice.Value
                    : variant.Price,
            Color = variant.Color,
            ColorCode = variant.ColorCode,
            ProductImageUrl = variant.Images.FirstOrDefault(i => i.IsPrimary)?.ImageUrl,
            IsPersonalised = isPersonalised,
            EngravingName = engravingName,
            EngravingDate = engravingDate,
            EngravingMessage = engravingMessage,
            EngravingPrice = engravingPrice
        };

        await _cartRepository.AddCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(string userId, CartUpdateDto cartUpdateDto)
    {
        if (cartUpdateDto.Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0.");
        }

        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new KeyNotFoundException("Cart not found");

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartUpdateDto.CartItemId);
        if (cartItem == null) throw new KeyNotFoundException("Cart item not found");

        var maxQty = _orderSettingsOptions.MaxNormalOrderQuantity;

        var otherItemsQuantity = cart.CartItems
            .Where(ci => ci.VariantId == cartItem.VariantId && ci.CartItemId != cartItem.CartItemId)
            .Sum(ci => ci.Quantity);

        var proposedQuantity = otherItemsQuantity + cartUpdateDto.Quantity;

        if (proposedQuantity > maxQty)
        {
            throw new InvalidOperationException($"Maximum quantity per product is {maxQty}. For larger quantities, please use Corporate & Bulk Orders.");
        }

        var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
        var variant = product?.Variants.FirstOrDefault(v => v.Id == cartItem.VariantId);
        if (variant != null && variant.StockQuantity < proposedQuantity)
        {
            throw new InvalidOperationException($"Only {variant.StockQuantity} units are available in stock.");
        }

        cartItem.Quantity = cartUpdateDto.Quantity;
        await _cartRepository.UpdateCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task RemoveCartItemAsync(string userId, int cartItemId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new KeyNotFoundException("Cart not found");

        await _cartRepository.RemoveCartItemAsync(cartItemId);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new KeyNotFoundException("Cart not found");

        await _cartRepository.ClearCartAsync(cart.CartId);
        await _cartRepository.SaveChangesAsync();
    }
}