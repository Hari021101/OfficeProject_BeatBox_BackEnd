using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponseDto>();

        CreateMap<CategoryCreateDto, Category>();

        CreateMap<CategoryUpdateDto, Category>();
    }
}