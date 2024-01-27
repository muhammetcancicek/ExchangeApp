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

namespace Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<EditUserDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class EditUserCommandHandler : IRequestHandler<EditUserCommand, EditUserDTO>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public EditUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<EditUserDTO> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                User? existingUser = await _userRepository.GetAsync(b => b.Id == request.Id);
                _userBusinessRules.UserShouldExistWhenRequested(existingUser);

                await _userBusinessRules.UserNameCanNotBeDuplicatedWhenEdit(request.Name, request.Id);

                _mapper.Map(request, existingUser);
                User updatedUser = await _userRepository.UpdateAsync(existingUser);

                EditUserDTO editedUserDto = _mapper.Map<EditUserDTO>(updatedUser);

                return editedUserDto;
            }
        }
    }
}
