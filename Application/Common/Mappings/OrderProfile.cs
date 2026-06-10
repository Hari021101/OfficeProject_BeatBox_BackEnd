using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Common.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.OrderDate,
                opt => opt.MapFrom(src => src.CreatedDate))

            .ForMember(dest => dest.PaymentMethod,
                opt => opt.MapFrom(src =>
                    src.Payments.Any()
                        ? src.Payments.OrderByDescending(p => p.CreatedDate).First().Method
                        : "COD"))

            .ForMember(dest => dest.PaymentStatus,
                opt => opt.MapFrom(src =>
                    src.Payments.Any()
                        ? src.Payments.OrderByDescending(p => p.CreatedDate).First().Status
                        : "Pending"));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src =>
                    src.Product != null
                        ? src.Product.Name
                        : "Unknown Product"));
    }
}