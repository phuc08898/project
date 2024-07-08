using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.PriceListUC.Command.UpdatePriceListCommand
{
    public class UpdatePriceListCommandValidator : AbstractValidator<UpdatePriceListCommand>
    {
        public UpdatePriceListCommandValidator() 
        {
            RuleFor(e => e.Arg.Name)
                .NotEmpty()
                .WithMessage("Price list name is required")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Price list name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => new { e.Arg.BeginDay, e.Arg.EndDay })
                .NotEmpty()
                .WithMessage("Begin day is required")
                .NotEmpty()
                .WithMessage("End day is required")
                .Must(compare => compare.BeginDay != compare.EndDay)
                .WithMessage("BeginDay and EndDay cannot be the same")
                .Must(compare => DateTimeOffset.Compare(compare.BeginDay, compare.EndDay) < 0)
                .WithMessage("BeginDay must be less than EndDay");
            RuleForEach(e => e.Arg.UpdatePriceListDetailArgs).ChildRules(PriceListDetail =>
            {
                PriceListDetail.RuleFor(p => p.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be greater than or equal to zero");
            });
        }
    }
}
