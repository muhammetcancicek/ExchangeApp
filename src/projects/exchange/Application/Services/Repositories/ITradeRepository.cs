using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Services.Repositories
{
    public interface ITradeRepository:IAsyncRepository<Trade>,IRepository<Trade>
    {
    }
}
