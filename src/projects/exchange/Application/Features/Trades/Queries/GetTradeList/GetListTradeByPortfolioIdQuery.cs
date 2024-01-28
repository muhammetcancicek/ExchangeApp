using Application.Features.Trades.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Queries.GetListTrade
{
    public class GetListTradeByPortfolioIdQuery : IRequest<TradeListModel>
    {
        public int PortfolioId { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListTradeByPortfolioIdQueryHandler : IRequestHandler<GetListTradeByPortfolioIdQuery, TradeListModel>
        {
            private readonly ITradeRepository _tradeRepository;
            private readonly IMapper _mapper;

            public GetListTradeByPortfolioIdQueryHandler(ITradeRepository tradeRepository, IMapper mapper)
            {
                _tradeRepository = tradeRepository;
                _mapper = mapper;
            }

            public async Task<TradeListModel> Handle(GetListTradeByPortfolioIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Trade> trades = await _tradeRepository.GetListAsync(
                    predicate: s => s.PortfolioId == request.PortfolioId, 
                    index: request.PageRequest.Page, 
                    size: request.PageRequest.PageSize,
                    include: query => query.Include(trade => trade.Share)
                           .Include(trade => trade.Portfolio));

                TradeListModel mappedTradeListModel = _mapper.Map<TradeListModel>(trades);

                return mappedTradeListModel;
            }
        }
    }
}
