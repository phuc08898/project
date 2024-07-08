using FluentValidation;
using Mapster;
using SGS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.PriceListUC.Command.CreatePriceListCommand
{
    public class CreatePriceListCommandValidator : AbstractValidator<CreatePriceListCommand>
    {
        public CreatePriceListCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Price list name is required")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Price list name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => new { e.BeginDay, e.EndDay })
                .NotEmpty()
                .WithMessage("Begin day is required")
                .NotEmpty()
                .WithMessage("End day is required")
                .Must(compare => compare.BeginDay != compare.EndDay)
                .WithMessage("BeginDay and EndDay cannot be the same")
                .Must(compare => DateTimeOffset.Compare(compare.BeginDay,compare.EndDay)<0 )
                .WithMessage("BeginDay must be less than EndDay");
            RuleForEach(e => e.PriceListDetailArgs).ChildRules(PriceListDetail =>
            {
                PriceListDetail.RuleFor(p => p.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be greater than or equal to zero");
            });
        }
    }
}
