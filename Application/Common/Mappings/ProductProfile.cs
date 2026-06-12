using Application.DTOs;
using AutoMapper;
using Domain.Entities;

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
                    : string.Empty))
            .ForMember(dest => dest.AverageRating,
        opt => opt.MapFrom(src =>
            src.Reviews.Any()
                ? src.Reviews.Average(r => r.Rating)
                : 0))

            .ForMember(dest => dest.ReviewCount,
        opt => opt.MapFrom(src =>
            src.Reviews.Count))

            .ForMember(dest => dest.Reviews,
        opt => opt.MapFrom(src => src.Reviews))

            .ForMember(dest => dest.Images,
        opt => opt.MapFrom(src => src.Images))

            .ForMember(dest => dest.Faqs,
        opt => opt.MapFrom(src => src.Faqs));

        // Create DTO -> Product
        CreateMap<ProductCreateDto, Product>();

        // Update DTO -> Product
        CreateMap<ProductUpdateDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Product -> Update DTO
        CreateMap<Product, ProductUpdateDto>();

        CreateMap<ProductReview, ProductReviewDto>()
    .ForMember(dest => dest.UserName,
        opt => opt.MapFrom(src => src.User.FullName));

        CreateMap<ProductImage, ProductImageDto>();

        CreateMap<ProductFaq, ProductFaqDto>();
    }
}