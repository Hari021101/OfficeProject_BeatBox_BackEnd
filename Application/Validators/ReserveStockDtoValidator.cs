using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class ReserveStockDtoValidator : AbstractValidator<ReserveStockDto>
{
    public ReserveStockDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
