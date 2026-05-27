using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class UserProfileProfile : Profile
{
    public UserProfileProfile()
    {
        CreateMap<AppUser, UserProfileDto>();
        CreateMap<UserAddress, UserAddressDto>().ReverseMap();
        
        CreateMap<WishlistItem, WishlistItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ProductDiscountPrice, opt => opt.MapFrom(src => src.Product.DiscountPrice));
    }
}
