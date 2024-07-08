using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.CategoryUC.Command.CreateCategoryCommand
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Category name is required")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Category name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => e.Slug)
                .NotEmpty()
                .WithMessage("Category slug is required")
                .Must(slug => slug.All(c => char.IsLetterOrDigit(c) || c == '-'))
                .WithMessage("Category slug can only contain letters, digits, spaces, and dashes");
        }
    }
}
