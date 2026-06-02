using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateStockDtoValidator : AbstractValidator<UpdateStockDto>
{
    public UpdateStockDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).NotEqual(0).WithMessage("Quantity cannot be zero.");
    }
}
