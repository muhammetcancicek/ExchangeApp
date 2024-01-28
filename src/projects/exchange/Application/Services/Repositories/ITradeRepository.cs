using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;


namespace Application.Services.Repositories
{
    public interface ITradeRepository:IAsyncRepository<Trade>,IRepository<Trade>
    {
        Task<bool> DoesUserHaveTrades(int userId);
        Task<List<Trade>> GetUserTradesByUserId(int userId);
        Task<Trade?> GetLatestPriceForBuyByShareId(int shareId);
        Task<Trade?> GetLatestPriceForSellByShareId(int shareId);
        IQueryable<Trade> GetByShareAndPortfolioId(int shareId, int portfolioId);
        //Task<int> GetNetQuantityByShareIdAndPortfolioId(int shareId, int portfolioId);
        Task<Dictionary<TradeType, int>> GetTotalSharesByTradeTypeAsync(int shareId, int portfolioId);
    }
}
