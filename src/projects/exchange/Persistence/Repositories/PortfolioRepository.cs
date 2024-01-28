using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PortfolioRepository : EfRepositoryBase<Portfolio, BaseDbContext>, IPortfolioRepository
    {
        public PortfolioRepository(BaseDbContext context) : base(context)
        {
        }
        public async Task<bool> PortfolioHasShare(int portfolioId, int shareId)
        {
            return Context.Portfolios.Any(s => s.Id == portfolioId && s.Trades.Any(m => m.ShareId == shareId));
        }
    }

}
