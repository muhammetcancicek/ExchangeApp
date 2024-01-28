using Application.Services.BusinessObjects;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Rules
{
    public class TradeBusinessRules
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IShareRepository _shareRepository;
        private readonly ITradeBO _tradeBO;

        public TradeBusinessRules(ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository, IShareRepository shareRepository,ITradeBO tradeBO)
        {
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
            _shareRepository = shareRepository;
            _tradeBO = tradeBO;
        }

        public void TradeShouldExistWhenRequested(Trade trade)
        {
            if (trade == null) throw new BusinessException("Requested trade does not exist");
        }
        public async Task PortfolioShouldBeExist(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetAsync(s=> s.Id == portfolioId);
            if (portfolio == null) throw new BusinessException("Portfolio is not exist");
        }
        public async Task ShareShouldBeExist(int shareId)
        {
            var share = await _shareRepository.GetAsync(s => s.Id == shareId);
            if (share == null) throw new BusinessException("Share is not exist!");
        }
        public void ShareShouldBeExist(Share? share)
        {
            if (share == null) throw new BusinessException("Share is not exist!");
        }
        public void PortfolioShouldBeExist(Portfolio? portfolio)
        {
            if (portfolio == null) throw new BusinessException("Portfolio is not exist");
        }

        public async Task UnitPriceShouldEqualOrMoreThenMaxPriceWhenBuy(double unitPrice, int shareId)
        {
            var minPrice = await _tradeBO.GetMinTradePriceWhenBuyByShareId(shareId);
            if (minPrice > unitPrice) throw new BusinessException(String.Format("The current price is at least {0:C2} per unit.", minPrice));
        }
        public async Task UnitPriceShouldEqualOrMoreThenMaxPriceWhenSell(double unitPrice, int shareId)
        {
            var minPrice = await _tradeBO.GetMinTradePriceWhenSellByShareId(shareId);
            if (minPrice > unitPrice) throw new BusinessException(String.Format("The current price is at least {0:C2} per unit.", minPrice));
        }
        public async Task PortfolioShouldHasShare(int portfolioId, int shareId)
        {
            var hasShare = await _portfolioRepository.PortfolioHasShare(portfolioId, shareId);
            if (!hasShare) throw new BusinessException("You dont have this share in your portfolio!");
        }
        public async Task PortfolioShouldHasEnoughShare(int portfolioId, int shareId)
        {
            var hasPortfolio = await _portfolioRepository.PortfolioHasShare(portfolioId, shareId);
            if (!hasPortfolio) throw new BusinessException("You dont have a portfolio!");
        }
        public async Task QuantityShouldEqualOrLessThenMaxPriceWhenSell(double quantity, int shareId, int portfolioId)
        {
            var shareQuantity = await _tradeBO.GetShareQuantityInPortfolio(shareId, portfolioId);
            if (quantity > shareQuantity) throw new BusinessException(String.Format("Your portfolio contains {0} shares", shareQuantity));
        }

    }
}







