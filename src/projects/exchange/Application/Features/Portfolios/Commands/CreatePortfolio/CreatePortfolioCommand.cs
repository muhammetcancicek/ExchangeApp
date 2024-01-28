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

namespace Application.Features.Portfolios.Commands.CreatePortfolio
{
    public class CreatePortfolioCommand : IRequest<CreatePortfolioDTO>
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, CreatePortfolioDTO>
        {
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;
            private readonly PortfolioBusinessRules _portfolioBusinessRules;

            public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository, IMapper mapper, PortfolioBusinessRules portfolioBusinessRules)
            {
                _portfolioRepository = portfolioRepository;
                _mapper = mapper;
                _portfolioBusinessRules = portfolioBusinessRules;
            }

            public async Task<CreatePortfolioDTO> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
            {
                await _portfolioBusinessRules.PortfolioTitleCanNotBeDuplicatedWhenInserted(request.Title);

                Portfolio mappedPortfolio = _mapper.Map<Portfolio>(request);
                Portfolio createdPortfolio = await _portfolioRepository.AddAsync(mappedPortfolio);
                CreatePortfolioDTO createdPortfolioDto = _mapper.Map<CreatePortfolioDTO>(createdPortfolio);

                return createdPortfolioDto;
            }
        }
    }
}
