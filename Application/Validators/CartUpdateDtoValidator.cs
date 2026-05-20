using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CartUpdateDtoValidator : AbstractValidator<CartUpdateDto>
{
    public CartUpdateDtoValidator()
    {
        RuleFor(x => x.CartItemId).GreaterThan(0).WithMessage("CartItemId must be greater than 0.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}