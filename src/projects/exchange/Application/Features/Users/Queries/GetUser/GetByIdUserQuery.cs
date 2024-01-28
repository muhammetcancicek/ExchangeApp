using Application.Features.Portfolios.Rules;
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
    public class GetByIdUserQuery : IRequest<UserGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserGetByIdDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            UserBusinessRules _userBusinessRules;

            public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserGetByIdDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(b => b.Id == request.Id);
                _userBusinessRules.UserShouldExistWhenRequested(user);

                UserGetByIdDto UserGetByIdDto = _mapper.Map<UserGetByIdDto>(user);
                return UserGetByIdDto;
            }
        }
    }
}
