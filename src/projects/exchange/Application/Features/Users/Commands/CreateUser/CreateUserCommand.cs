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

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserDTO>
    {
        public string Name { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDTO>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreateUserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserNameCanNotBeDuplicatedWhenInserted(request.Name);

                User mappedUser = _mapper.Map<User>(request);
                User createdUser = await _userRepository.AddAsync(mappedUser);
                CreateUserDTO createdUserDto = _mapper.Map<CreateUserDTO>(createdUser);

                return createdUserDto;
            }
        }
    }
}
