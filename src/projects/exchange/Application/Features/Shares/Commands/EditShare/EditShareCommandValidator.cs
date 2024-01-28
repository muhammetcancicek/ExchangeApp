using Application.Features.Shares.Commands.EditShare;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.EditBrand
{
    public class EditShareCommandValidator : AbstractValidator<EditShareCommand>
    {
        public EditShareCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Symbol)
                .NotEmpty()
                .Length(3)
                .Must(BeAllUpperAndNoTurkishChars).WithMessage("{PropertyName} should be all uppercase and contain no Turkish characters")
                .WithMessage("{PropertyName} must be exactly 3 characters long");
        }

        private bool BeAllUpperAndNoTurkishChars(string symbol)
        {
            return symbol.All(character => char.IsUpper(character) && IsAscii(character));
        }

        private bool IsAscii(char character)
        {
            return character >= 'A' && character <= 'Z';
        }
    }
}
