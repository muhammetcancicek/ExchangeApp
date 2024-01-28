using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BusinessObjects
{
    public interface ITradeBO
    {
        Task<Double> GetMinTradePriceWhenBuyByShareId(int shareId);
        Task<Double> GetMinTradePriceWhenSellByShareId(int shareId);
        Task<int> GetShareQuantityInPortfolio(int shareId, int portfolioId);
    }
}
