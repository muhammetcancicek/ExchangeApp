using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUser
{
    public class DeleteUserQuery : IRequest<bool>
    {
        public int Id { get; set; }

        public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery, bool>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public DeleteUserQueryHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<bool> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(b => b.Id == request.Id);
                _userBusinessRules.UserShouldExistWhenRequested(user);
                await _userBusinessRules.TradeShouldNotExistWhenUserDeleted(request.Id);

                await _userRepository.DeleteAsync(user);
                return true;
            }
        }
    }
}
