using Application.Features.Portfolios.Dtos;
using Application.Features.Portfolios.Rules;
using Application.Features.Shares.Rules;
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
    public class GetByIdPortfolioQuery : IRequest<PortfolioGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdPortfolioQueryHandler : IRequestHandler<GetByIdPortfolioQuery, PortfolioGetByIdDto>
        {
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;
            PortfolioBusinessRules _portfolioBusinessRules;

            public GetByIdPortfolioQueryHandler(IPortfolioRepository portfolioRepository, IMapper mapper, PortfolioBusinessRules portfolioBusinessRules)
            {
                _portfolioRepository = portfolioRepository;
                _mapper = mapper;
                _portfolioBusinessRules = portfolioBusinessRules;
            }

            public async Task<PortfolioGetByIdDto> Handle(GetByIdPortfolioQuery request, CancellationToken cancellationToken)
            {
                Portfolio? portfolio = await _portfolioRepository.GetAsync(b => b.Id == request.Id);
                _portfolioBusinessRules.PortfolioShouldExistWhenRequested(portfolio);

                PortfolioGetByIdDto PortfolioGetByIdDto = _mapper.Map<PortfolioGetByIdDto>(portfolio);
                return PortfolioGetByIdDto;
            }
        }
    }
}
