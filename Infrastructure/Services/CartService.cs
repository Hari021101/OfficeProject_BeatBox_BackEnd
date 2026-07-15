using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IMapper mapper,IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<CartDto> GetCartAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task AddToCartAsync(string userId, CartAddDto cartAddDto)
    {
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
        var product =
         await _productRepository.GetByIdAsync(
         cartAddDto.ProductId);

        if (product == null)
            throw new Exception("Product not found");

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
            throw new Exception($"Variant not found for product {product.Id}. Provide a valid VariantId.");

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
                throw new Exception("Custom engraving is not available for this product.");
            }

            if (string.IsNullOrWhiteSpace(cartAddDto.EngravingName))
            {
                throw new Exception("Engraving name is required for personalized products.");
            }

            if (cartAddDto.EngravingName.Length > 12)
            {
                throw new Exception("Engraving name cannot exceed 12 characters.");
            }

            if (!string.IsNullOrEmpty(cartAddDto.EngravingMessage) && cartAddDto.EngravingMessage.Length > 40)
            {
                throw new Exception("Engraving message cannot exceed 40 characters.");
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
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new Exception("Cart not found");

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartUpdateDto.CartItemId);
        if (cartItem == null) throw new Exception("Cart item not found");

        cartItem.Quantity = cartUpdateDto.Quantity;
        await _cartRepository.UpdateCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task RemoveCartItemAsync(string userId, int cartItemId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new Exception("Cart not found");

        await _cartRepository.RemoveCartItemAsync(cartItemId);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new Exception("Cart not found");

        await _cartRepository.ClearCartAsync(cart.CartId);
        await _cartRepository.SaveChangesAsync();
    }
}