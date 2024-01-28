using Application.Features.Shares.Dtos;
using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Services.Repositories
{
    public interface IShareRepository:IAsyncRepository<Share>,IRepository<Share>
    {
        Task<IQueryable<ShareListDtoWithPrice>> GetSharesWithLatestPricesAsync();
    }
}
