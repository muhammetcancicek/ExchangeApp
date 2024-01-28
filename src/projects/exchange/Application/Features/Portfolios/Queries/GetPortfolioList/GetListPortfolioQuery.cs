using Application.Features.Portfolios.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Queries.GetListPortfolio
{
    public class GetListPortfolioQuery:IRequest<PortfolioListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListPortfolioQueryHandler : IRequestHandler<GetListPortfolioQuery, PortfolioListModel>
        {
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;

            public GetListPortfolioQueryHandler(IPortfolioRepository portfolioRepository, IMapper mapper)
            {
                _portfolioRepository = portfolioRepository;
                _mapper = mapper;
            }

            public async Task<PortfolioListModel> Handle(GetListPortfolioQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Portfolio> portfolios = await _portfolioRepository.GetListAsync(index: request.PageRequest.Page,size:request.PageRequest.PageSize);

                PortfolioListModel mappedPortfolioListModel = _mapper.Map<PortfolioListModel>(portfolios);

                return mappedPortfolioListModel;
            }
        }
    }
}
