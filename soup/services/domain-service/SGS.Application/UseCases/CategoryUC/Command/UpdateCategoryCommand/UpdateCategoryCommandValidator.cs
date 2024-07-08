using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.CategoryUC.Command.UpdateCategoryCommand
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(e => e.Arg.Name)
                .NotEmpty()
                .WithMessage("Category name is required")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Category name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => e.Arg.Slug)
                .NotEmpty()
                .WithMessage("Category slug is required")
                .Must(slug => slug.All(c => char.IsLetterOrDigit(c) || c == '-'))
                .WithMessage("Category slug can only contain letters, digits, spaces, and dashes");
        }
    }
}
