using FluentValidation;
using SGS.Application.UseCases.ProductUC.CreateProductCommand;
using SGS.Domain.Common.Utils;
using System.Xml.Linq;

namespace SGS.Application.UseCases.ProductUC.CreateProductCommand;
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Product name is required")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Product name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => e.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be greater than zero");
            RuleFor(e => e.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id is required");
            RuleFor(e => e.Slug)
                .NotEmpty()
                .WithMessage("Product slug is required")
                .Must(slug => slug.All(c => char.IsLetterOrDigit(c) || c == '-'))
                .WithMessage("Product slug can only contain letters, digits, spaces, and dashes");
            RuleFor(e => e.Price)
                .NotEmpty()
                .WithMessage("Product price is required")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than zero");
            RuleForEach(e => e.Variants).ChildRules(variant =>
            {
                variant.RuleFor(v => v.Name)
                    .Must(name => name.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '_'))
                    .WithMessage("Variant name can only contain letters, digits, spaces, dashes, and underscores");

                variant.RuleFor(v => v.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Variant price must be greater than or equal to zero");
            });
    }

    }


