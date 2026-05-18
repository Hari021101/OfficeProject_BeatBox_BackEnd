using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product -> Response DTO
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null
                    ? src.Category.Name
                    : string.Empty));

        // Create DTO -> Product
        CreateMap<ProductCreateDto, Product>();

        // Update DTO -> Product
        CreateMap<ProductUpdateDto, Product>();

        // Product -> Update DTO
        CreateMap<Product, ProductUpdateDto>();
    }
}