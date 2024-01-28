using Application.Features.Portfolios.Commands.EditPortfolio;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.EditBrand
{
    public class EditPortfolioCommandValidator : AbstractValidator<EditPortfolioCommand>
    {
        public EditPortfolioCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}
