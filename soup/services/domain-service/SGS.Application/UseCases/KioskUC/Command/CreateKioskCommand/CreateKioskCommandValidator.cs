using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Command.CreateKioskCommand
{
    public class CreateKioskCommandValidator : AbstractValidator<CreateKioskCommand>
    {
        public CreateKioskCommandValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Kiosk name is required!")
                .NotNull()
                .WithMessage("Kiosk name is not null")
                .Must(name => name.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '_'))
                .WithMessage("Product name can only contain letters, digits, spaces, and dashes");
            RuleFor(e => e.Address)
                .NotNull()
                .WithMessage("Kiosk address is not null")
                .NotEmpty()
                .WithMessage("Kiosk address is required!");
            RuleFor(e => e.Phonenumber)
                .NotEmpty()
                .WithMessage("Kiosk phone number is required!")
                .NotNull()
                .WithMessage("Kiosk phone number is not null")
                .Must(phone => phone.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ' || c == '(' || c == ')'))
                .WithMessage("Kiosk phone number can only contain digits, +, -, space, (, and )");
        }
    }
}
