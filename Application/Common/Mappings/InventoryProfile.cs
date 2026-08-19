using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryDto>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : string.Empty))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Product != null && s.Product.Category != null ? s.Product.Category.Name : string.Empty))
            .ReverseMap();
    }
}
