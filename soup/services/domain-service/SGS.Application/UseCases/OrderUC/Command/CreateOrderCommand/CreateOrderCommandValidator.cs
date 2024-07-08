using FluentValidation;
using SGS.Domain.Enums;

namespace SGS.Application.UseCases.OrderUC.Command.CreateOrderCommand;


public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .WithMessage("Amount Must be greater than zero");
        RuleFor(e => e.PaymentMethod)
            .NotEmpty()
            .WithMessage("PaymentMethod Must not be empty")
            .Must(e => PaymentMethods.IsEnum(e))
            .WithMessage("PaymentMethod Not supported");
    }

}