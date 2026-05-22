using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImage,
                    opt => opt.MapFrom(src => src.Product.ImageUrl));
        }
    }
}