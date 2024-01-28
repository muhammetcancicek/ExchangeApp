using Application.Features.Portfolios.Dtos;
using Application.Features.Portfolios.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Queries.GetPortfolio
{
    public class DeletePortfolioQuery : IRequest<bool>
    {
        public int Id { get; set; }

        public class DeletePortfolioQueryHandler : IRequestHandler<DeletePortfolioQuery, bool>
        {
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;
            private readonly PortfolioBusinessRules _portfolioBusinessRules;

            public DeletePortfolioQueryHandler(IPortfolioRepository portfolioRepository, IMapper mapper, PortfolioBusinessRules portfolioBusinessRules)
            {
                _portfolioRepository = portfolioRepository;
                _mapper = mapper;
                _portfolioBusinessRules = portfolioBusinessRules;
            }

            public async Task<bool> Handle(DeletePortfolioQuery request, CancellationToken cancellationToken)
            {
                Portfolio? portfolio = await _portfolioRepository.GetAsync(b => b.Id == request.Id);
                _portfolioBusinessRules.PortfolioShouldExistWhenRequested(portfolio);
                await _portfolioBusinessRules.TradeShouldNotExistWhenPortfolioDeleted(request.Id);

                await _portfolioRepository.DeleteAsync(portfolio);
                return true;
            }
        }
    }
}
