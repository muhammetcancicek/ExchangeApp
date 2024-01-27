using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Services.Repositories
{
    public interface ITradeRepository:IAsyncRepository<Trade>,IRepository<Trade>
    {
        Task<bool> DoesUserHaveTrades(int userId);
        Task<List<Trade>> GetUserTradesByUserId(int userId);
    }
}
