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

namespace Application.Features.Portfolios.Commands.EditPortfolio
{
    public class EditPortfolioCommand : IRequest<EditPortfolioDTO>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public class EditPortfolioCommandHandler : IRequestHandler<EditPortfolioCommand, EditPortfolioDTO>
        {
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;
            private readonly PortfolioBusinessRules _portfolioBusinessRules;

            public EditPortfolioCommandHandler(IPortfolioRepository portfolioRepository, IMapper mapper, PortfolioBusinessRules portfolioBusinessRules)
            {
                _portfolioRepository = portfolioRepository;
                _mapper = mapper;
                _portfolioBusinessRules = portfolioBusinessRules;
            }

            public async Task<EditPortfolioDTO> Handle(EditPortfolioCommand request, CancellationToken cancellationToken)
            {
                Portfolio? existingPortfolio = await _portfolioRepository.GetAsync(b => b.Id == request.Id);
                _portfolioBusinessRules.PortfolioShouldExistWhenRequested(existingPortfolio);

                await _portfolioBusinessRules.PortfolioTitleCanNotBeDuplicatedWhenEdit(request.Title, request.Id);

                _mapper.Map(request, existingPortfolio);
                Portfolio updatedPortfolio = await _portfolioRepository.UpdateAsync(existingPortfolio);

                EditPortfolioDTO editedPortfolioDto = _mapper.Map<EditPortfolioDTO>(updatedPortfolio);

                return editedPortfolioDto;
            }
        }
    }
}
