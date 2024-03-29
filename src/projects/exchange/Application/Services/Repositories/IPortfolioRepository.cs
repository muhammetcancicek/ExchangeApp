﻿using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Services.Repositories
{
    public interface IPortfolioRepository:IAsyncRepository<Portfolio>,IRepository<Portfolio>
    {
        Task<bool> PortfolioHasShare(int portfolioId, int shareId);
    }
}
