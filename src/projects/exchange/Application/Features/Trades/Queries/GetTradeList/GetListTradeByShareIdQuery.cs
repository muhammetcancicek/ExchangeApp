using Application.Features.Trades.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Trades.Queries.GetListTrade
{
    public class GetListTradeByShareIdQuery : IRequest<TradeListModel>
    {
        public int ShareId { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListTradeQueryByShareIdHandler : IRequestHandler<GetListTradeByShareIdQuery, TradeListModel>
        {
            private readonly ITradeRepository _tradeRepository;
            private readonly IMapper _mapper;

            public GetListTradeQueryByShareIdHandler(ITradeRepository tradeRepository, IMapper mapper)
            {
                _tradeRepository = tradeRepository;
                _mapper = mapper;
            }

            public async Task<TradeListModel> Handle(GetListTradeByShareIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Trade> trades = await _tradeRepository.GetListAsync(
                    predicate: s => s.ShareId == request.ShareId,
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
