using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateStockDtoValidator : AbstractValidator<UpdateStockDto>
{
    public UpdateStockDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to zero.");
    }
}
