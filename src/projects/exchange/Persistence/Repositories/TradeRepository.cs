using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TradeRepository : EfRepositoryBase<Trade, BaseDbContext>, ITradeRepository
    {
        public TradeRepository(BaseDbContext context) : base(context)
        {
        }
        public async Task<List<Trade>> GetUserTradesByUserId(int userId)
        {
            return await Context.Trades
                .Where(trade => trade.Portfolio.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> DoesUserHaveTrades(int userId)
        {
            return await Context.Trades
                .AnyAsync(trade => trade.Portfolio.UserId == userId);
        }
        public async Task<Trade?> GetLatestPriceForBuyByShareId(int shareId)
        {
            return await Context.Trades
                .Where(trade => trade.ShareId == shareId && trade.TradeType == Domain.Enums.TradeType.Sell)
                .OrderByDescending(trade => trade.CreateDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Trade?> GetLatestPriceForSellByShareId(int shareId)
        {
            return await Context.Trades
                .Where(trade => trade.ShareId == shareId && trade.TradeType == Domain.Enums.TradeType.Buy)
                .OrderByDescending(trade => trade.CreateDate)
                .FirstOrDefaultAsync();
        }
        public  IQueryable<Trade> GetByShareAndPortfolioId(int shareId, int portfolioId)
        {
            return Context.Trades
                .Where(trade => trade.ShareId == shareId && trade.PortfolioId == portfolioId);
        }
        //private async Task<int> GetNetBuyQuantityByShareIdAndPortfolioId(int shareId, int portfolioId)
        //{
        //    var buyQuantity = await Context.Trades
        //                                   .Where(trade => trade.ShareId == shareId && trade.PortfolioId == portfolioId && trade.TradeType == Domain.Enums.TradeType.Buy)
        //                                   .SumAsync(trade => (int?)trade.Quantity) ?? 0;
        //    return buyQuantity;
        //}
        //private async Task<int> GetNetSellQuantityByShareIdAndPortfolioId(int shareId, int portfolioId)
        //{
        //    var sellQuantity = await Context.Trades
        //                                    .Where(trade => trade.ShareId == shareId && trade.PortfolioId == portfolioId && trade.TradeType == Domain.Enums.TradeType.Sell)
        //                                    .SumAsync(trade => (int?)trade.Quantity) ?? 0;

        //    return sellQuantity;
        //}
        //public async Task<int> GetNetQuantityByShareIdAndPortfolioId(int shareId, int portfolioId)
        //{
        //    return await GetNetSellQuantityByShareIdAndPortfolioId(shareId, portfolioId) - await GetNetQuantityByShareIdAndPortfolioId(shareId, portfolioId);
        //}
        public async Task<Dictionary<TradeType, int>> GetTotalSharesByTradeTypeAsync(int shareId, int portfolioId)
        {
            return await Context.Trades
                                .Where(trade => trade.ShareId == shareId && trade.PortfolioId == portfolioId)
                                .GroupBy(trade => trade.TradeType)
                                .Select(group => new
                                {
                                    TradeType = group.Key,
                                    TotalQuantity = group.Sum(trade => trade.Quantity)
                                })
                                .ToDictionaryAsync(k => k.TradeType, v => v.TotalQuantity);
        }
    }

}
