using Application.Features.Shares.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Paging;
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
    public class ShareRepository : EfRepositoryBase<Share, BaseDbContext>, IShareRepository
    {
        public ShareRepository(BaseDbContext context) : base(context)
        {
        }
        public async Task<IQueryable<ShareListDtoWithPrice>> GetSharesWithLatestPricesAsync()
        {
            var shares = Context.Shares.Select(share => new ShareListDtoWithPrice
            {
                Id = share.Id,
                Symbol = share.Symbol,
                Name = share.Name,
                LastBuyPrice = Context.Trades
                    .Where(t => t.ShareId == share.Id && t.TradeType == Domain.Enums.TradeType.Buy)
                    .OrderByDescending(t => t.CreateDate)
                    .Select(t => t.UnitPrice)
                    .FirstOrDefault(),
                LastSellPrice = Context.Trades
                    .Where(t => t.ShareId == share.Id && t.TradeType == Domain.Enums.TradeType.Sell)
                    .OrderByDescending(t => t.CreateDate)
                    .Select(t => t.UnitPrice)
                    .FirstOrDefault()
            });
            return shares;
        }
    }

}
