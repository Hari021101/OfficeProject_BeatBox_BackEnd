using Application.Common.Options;
using Application.DTOs;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Validators;

public class CartAddDtoValidator : AbstractValidator<CartAddDto>
{
    public CartAddDtoValidator(IOptions<OrderSettingsOptions> orderOptions)
    {
        var maxQty = orderOptions?.Value?.MaxNormalOrderQuantity ?? 15;
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(maxQty).WithMessage($"Maximum quantity per product is {maxQty}. For larger quantities, please use Corporate & Bulk Orders.");
    }

    public CartAddDtoValidator() : this(Options.Create(new OrderSettingsOptions()))
    {
    }
}