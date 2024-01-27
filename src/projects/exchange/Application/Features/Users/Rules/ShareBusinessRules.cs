using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly ITradeRepository _tradeRepository;

        public UserBusinessRules(IUserRepository userRepository, ITradeRepository tradeRepository)
        {
            _userRepository = userRepository;
            _tradeRepository = tradeRepository;
        }

        public async Task UserNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("User name exists.");
        }
        public async Task UserNameCanNotBeDuplicatedWhenEdit(string name, int id)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Name == name && b.Id != id);
            if (result.Items.Any()) throw new BusinessException("User name exists.");
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("Requested user does not exist");
        }
        public async Task TradeShouldNotExistWhenUserDeleted(int userId)
        {
           var hasTrade = await _tradeRepository.DoesUserHaveTrades(userId);
            if (hasTrade) throw new BusinessException("User is has trades, you can't delete it!");
        }
    }
}
