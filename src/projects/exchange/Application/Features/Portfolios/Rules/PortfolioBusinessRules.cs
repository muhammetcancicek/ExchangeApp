using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Rules
{
    public class PortfolioBusinessRules
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly ITradeRepository _tradeRepository;

        public PortfolioBusinessRules(IPortfolioRepository portfolioRepository, ITradeRepository tradeRepository)
        {
            _portfolioRepository = portfolioRepository;
            _tradeRepository = tradeRepository;
        }

        public async Task PortfolioTitleCanNotBeDuplicatedWhenInserted(string title)
        {
            IPaginate<Portfolio> result = await _portfolioRepository.GetListAsync(b => b.Title == title);
            if (result.Items.Any()) throw new BusinessException("Portfolio name exists.");
        }

        public async Task PortfolioTitleCanNotBeDuplicatedWhenEdit(string title, int id)
        {
            IPaginate<Portfolio> result = await _portfolioRepository.GetListAsync(b => b.Title == title && b.Id != id);
            if (result.Items.Any()) throw new BusinessException("Portfolio name exists.");
        }

        public void PortfolioShouldExistWhenRequested(Portfolio portfolio)
        {
            if (portfolio == null) throw new BusinessException("Requested portfolio does not exist");
        }
        public async Task TradeShouldNotExistWhenPortfolioDeleted(int portfolioId)
        {
           var list = await _tradeRepository.GetListAsync(b => b.PortfolioId == portfolioId);
            if (list.Count > 0) throw new BusinessException("Portfolio is has trades, you can't delete it!");
        }
    }
}
