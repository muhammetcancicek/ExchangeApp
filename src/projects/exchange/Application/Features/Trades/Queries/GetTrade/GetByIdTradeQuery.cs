using Application.Features.Portfolios.Rules;
using Application.Features.Trades.Dtos;
using Application.Features.Trades.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Queries.GetTrade
{
    public class GetByIdTradeQuery : IRequest<TradeGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdTradeQueryHandler : IRequestHandler<GetByIdTradeQuery, TradeGetByIdDto>
        {
            private readonly ITradeRepository _tradeRepository;
            private readonly IMapper _mapper;
            TradeBusinessRules _tradeBusinessRules;


            public GetByIdTradeQueryHandler(ITradeRepository tradeRepository, IMapper mapper, TradeBusinessRules tradeBusinessRules)
            {
                _tradeRepository = tradeRepository;
                _mapper = mapper;
                _tradeBusinessRules = tradeBusinessRules;
            }

            public async Task<TradeGetByIdDto> Handle(GetByIdTradeQuery request, CancellationToken cancellationToken)
            {
                Trade? trade = await _tradeRepository.GetAsync(b => b.Id == request.Id, 
                    include: q=> q.Include(s=> s.Share).Include(s=> s.Portfolio));
                _tradeBusinessRules.TradeShouldExistWhenRequested(trade);

                TradeGetByIdDto TradeGetByIdDto = _mapper.Map<TradeGetByIdDto>(trade);
                return TradeGetByIdDto;
            }
        }
    }
}
