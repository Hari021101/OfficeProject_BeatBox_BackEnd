using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile() 
        {
            CreateMap<Payment, PaymentDto>();

        }
    }
}
