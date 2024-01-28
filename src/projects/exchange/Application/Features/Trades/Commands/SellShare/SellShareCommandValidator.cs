using Application.Features.Trades.Commands.CreateTrade;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class SellShareCommandValidator : AbstractValidator<SellShareCommand>
    {
        public SellShareCommandValidator()
        {
            RuleFor(c => c.ShareId).NotEmpty().NotNull();
            RuleFor(c => c.PortfolioId).NotEmpty().NotNull();
            RuleFor(command => command.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(command => command.UnitPrice)
                .Must(BeAValidPrice)
                .WithMessage("Unit Price must be a double with only two decimal places");
        }

        private bool BeAValidPrice(double price)
        {
            return Math.Round(price, 2) == price;
        }
    }
}
