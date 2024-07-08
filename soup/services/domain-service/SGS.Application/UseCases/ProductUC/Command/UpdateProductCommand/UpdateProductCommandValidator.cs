using FluentValidation;
using SGS.Domain.Common.Utils;
using System.Xml.Linq;

namespace SGS.Application.UseCases.ProductUC.Command.UpdateProductCommand;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .Must(name => name.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || c == ' ' || c == '-' || c == '_'))
            .WithMessage("Product name can only contain letters, digits, spaces, and dashes");
        RuleFor(e => e.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id is required");
        RuleFor(e => e.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity must be greater than zero");
        RuleFor(e => e.Slug)
            .NotEmpty()
            .WithMessage("Product slug is required")
            .Must(slug => slug.All(c => char.IsLetterOrDigit(c) || c == '-'))
            .WithMessage("Product slug can only contain letters, digits, spaces, and dashes");
        RuleForEach(e => e.UpdateVariants).ChildRules(variant =>
        {
            variant.RuleFor(v => v.Name)
                .NotNull()
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Variant name can only contain letters, digits, spaces, dashes, and underscores");

            variant.RuleFor(v => v.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Variant price must be greater than or equal to zero");
        });
    }
}