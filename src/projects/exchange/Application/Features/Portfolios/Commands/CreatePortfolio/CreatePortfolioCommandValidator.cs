using Application.Features.Portfolios.Commands.CreatePortfolio;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreatePortfolioCommandValidator : AbstractValidator<CreatePortfolioCommand>
    {
        public CreatePortfolioCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().NotNull();
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}
