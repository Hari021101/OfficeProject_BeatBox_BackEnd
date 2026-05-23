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
        var product = await _productRepository.GetByIdAsync(cartAddDto.ProductId);

        if (product == null)
            throw new Exception("Product not found");

        decimal productPrice = product.Price;

        // Create cart item
        var cartItem = new CartItem
        {
            CartId = cart.CartId,
            ProductId = cartAddDto.ProductId,
            Quantity = cartAddDto.Quantity,
            UnitPrice = product.Price

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