using Application.Features.Trades.Dtos;
using Application.Features.Trades.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Commands.CreateTrade
{
    public class SellShareCommand : IRequest<BuyAndSellShareDTO>
    {
        public int PortfolioId { get; set; }
        public int ShareId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public class SellShareCommandHandler : IRequestHandler<SellShareCommand, BuyAndSellShareDTO>
        {
            private readonly ITradeRepository _tradeRepository;
            private readonly IShareRepository _shareRepository;
            private readonly IPortfolioRepository _portfolioRepository;
            private readonly IMapper _mapper;
            private readonly TradeBusinessRules _tradeBusinessRules;

            public SellShareCommandHandler(ITradeRepository tradeRepository, IMapper mapper, TradeBusinessRules tradeBusinessRules, IPortfolioRepository portfolioRepository, IShareRepository shareRepository)
            {
                _tradeRepository = tradeRepository;
                _mapper = mapper;
                _tradeBusinessRules = tradeBusinessRules;
                _portfolioRepository = portfolioRepository;
                _shareRepository = shareRepository;
            }

            public async Task<BuyAndSellShareDTO> Handle(SellShareCommand request, CancellationToken cancellationToken)
            {
                var share = await _shareRepository.GetAsync(s => s.Id == request.ShareId);
                var portfolio = await _portfolioRepository.GetAsync(s => s.Id == request.PortfolioId);
                _tradeBusinessRules.ShareShouldBeExist(share);
                _tradeBusinessRules.PortfolioShouldBeExist(portfolio);
                await _tradeBusinessRules.PortfolioShouldHasShare(request.PortfolioId, request.ShareId);
                await _tradeBusinessRules.QuantityShouldEqualOrLessThenMaxPriceWhenSell(request.Quantity, request.ShareId, request.PortfolioId);

                await _tradeBusinessRules.UnitPriceShouldEqualOrMoreThenMaxPriceWhenSell(request.UnitPrice, request.ShareId);

                Trade trade = new Trade
                {
                    CreateDate = DateTime.Now,
                    PortfolioId = request.PortfolioId,
                    Quantity = request.Quantity,
                    ShareId = request.ShareId,
                    TradeType = Domain.Enums.TradeType.Sell,
                    UnitPrice = request.UnitPrice,
                };
                Trade createdTrade = await _tradeRepository.AddAsync(trade);
                BuyAndSellShareDTO createdTradeDto = _mapper.Map<BuyAndSellShareDTO>(createdTrade);
                createdTradeDto.ShareName = share.Name;
                createdTradeDto.PortfolioTitle = portfolio.Title;
                return createdTradeDto;
            }
        }
    }
}
