using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Services.Repositories
{
    public interface IPortfolioRepository:IAsyncRepository<Portfolio>,IRepository<Portfolio>
    {
    }
}
