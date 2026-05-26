using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Order -> OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.OrderDate,
                opt => opt.MapFrom(src => src.CreatedDate));

        // OrderItem -> OrderItemDto  — includes ProductName from navigation property
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : "Unknown Product"));
    }
}