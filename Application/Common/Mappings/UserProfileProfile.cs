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
    .ForMember(
        dest => dest.ProductName,
        opt => opt.MapFrom(src => src.Product.Name)
    )
   .ForMember(
    dest => dest.ProductImage,
    opt => opt.MapFrom(src =>
        src.Product.Variants
            .SelectMany(v => v.Images)
            .Where(i => i.IsPrimary)
            .Select(i => i.ImageUrl)
            .FirstOrDefault()

        ??

        src.Product.Variants
            .SelectMany(v => v.Images)
            .Select(i => i.ImageUrl)
            .FirstOrDefault()
    )
)
    .ForMember(
        dest => dest.ProductPrice,
        opt => opt.MapFrom(src =>
            src.Product.Variants
                .Select(v => v.Price)
                .FirstOrDefault()
        )
    )
    .ForMember(
        dest => dest.ProductDiscountPrice,
        opt => opt.MapFrom(src =>
            src.Product.Variants
                .Select(v => v.DiscountPrice)
                .FirstOrDefault()
        )
    )

.ForMember(dest => dest.ProductPrice,
    opt => opt.MapFrom(src =>
        src.Product.Variants
            .Select(v => v.Price)
            .FirstOrDefault()))

.ForMember(dest => dest.ProductDiscountPrice,
    opt => opt.MapFrom(src =>
        src.Product.Variants
            .Select(v => (decimal?)v.DiscountPrice)
            .FirstOrDefault()));
    }
}
