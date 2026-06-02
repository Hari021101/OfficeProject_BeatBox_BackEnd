using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryDto>().ReverseMap();
    }
}
