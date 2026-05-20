using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("Shipping address is required.");
    }
}