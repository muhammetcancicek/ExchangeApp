using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Rules
{
    public class ShareBusinessRules
    {
        private readonly IShareRepository _shareRepository;
        private readonly ITradeRepository _tradeRepository;

        public ShareBusinessRules(IShareRepository shareRepository, ITradeRepository tradeRepository)
        {
            _shareRepository = shareRepository;
            _tradeRepository = tradeRepository;
        }

        public async Task ShareNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Share> result = await _shareRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Share name exists.");
        }

        public async Task ShareSymbolCanNotBeDuplicatedWhenInserted(string symbol)
        {
            IPaginate<Share> result = await _shareRepository.GetListAsync(b => b.Symbol == symbol);
            if (result.Items.Any()) throw new BusinessException("Share symbol exists.");
        }

        public async Task ShareNameCanNotBeDuplicatedWhenEdit(string name, int id)
        {
            IPaginate<Share> result = await _shareRepository.GetListAsync(b => b.Name == name && b.Id != id);
            if (result.Items.Any()) throw new BusinessException("Share name exists.");
        }

        public async Task ShareSymbolCanNotBeDuplicatedWhenEdit(string symbol, int id)
        {
            IPaginate<Share> result = await _shareRepository.GetListAsync(b => b.Symbol == symbol && b.Id != id);
            if (result.Items.Any()) throw new BusinessException("Share symbol exists.");
        }

        public void ShareShouldExistWhenRequested(Share share)
        {
            if (share == null) throw new BusinessException("Requested share does not exist");
        }
        public async Task TradeShouldNotExistWhenShareDeleted(int shareId)
        {
           var list = await _tradeRepository.GetListAsync(b => b.ShareId == shareId);
            if (list.Count > 0) throw new BusinessException("Share is has trades, you can't delete it!");
        }
    }
}
