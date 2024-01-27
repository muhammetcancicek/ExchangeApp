using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
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
    }

}
