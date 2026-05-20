using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PaymentProcessDtoValidator : AbstractValidator<PaymentProcessDto>
{
    public PaymentProcessDtoValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("OrderId must be greater than 0.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        RuleFor(x => x.Method).NotEmpty().WithMessage("Payment method is required.");
    }
}