using Application.Services.BusinessObjects;
using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessObjects
{
    public class TradeBO : ITradeBO
    {
        private readonly ITradeRepository _tradeRepository;
        public TradeBO(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }
        public async Task<Double> GetMinTradePriceWhenBuyByShareId(int shareId)
        {
            var trade = await _tradeRepository.GetLatestPriceForBuyByShareId(shareId);
            if (trade == null)
                return 0.00;

            return Math.Round(trade.UnitPrice, 2, MidpointRounding.AwayFromZero);
        }
        public async Task<Double> GetMinTradePriceWhenSellByShareId(int shareId)
        {
            var trade = await _tradeRepository.GetLatestPriceForSellByShareId(shareId);
            if (trade == null)
                return 0.00;

            return Math.Round(trade.UnitPrice, 2, MidpointRounding.AwayFromZero);
        }
        public async Task<int> GetShareQuantityInPortfolio(int shareId, int portfolioId)
        {
            var quantity = 0; 
            var trade = await _tradeRepository.GetTotalSharesByTradeTypeAsync(shareId, portfolioId);
            if (trade == null)
                return 0;

            if (trade.Where(s => s.Key == Domain.Enums.TradeType.Buy).Count() > 0)
                quantity += trade.FirstOrDefault(s => s.Key == Domain.Enums.TradeType.Buy).Value;
            if (trade.Where(s => s.Key == Domain.Enums.TradeType.Sell).Count() > 0)
                quantity -= trade.FirstOrDefault(s => s.Key == Domain.Enums.TradeType.Sell).Value;

            return quantity;
        }


    }
}
