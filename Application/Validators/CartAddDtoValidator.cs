using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CartAddDtoValidator : AbstractValidator<CartAddDto>
{
    public CartAddDtoValidator()
    {
        
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}