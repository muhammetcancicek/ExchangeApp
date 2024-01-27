using Application.Features.Shares.Dtos;
using Application.Features.Shares.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Commands.CreateShare
{
    public class CreateShareCommand : IRequest<CreateShareDTO>
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public class CreateShareCommandHandler : IRequestHandler<CreateShareCommand, CreateShareDTO>
        {
            private readonly IShareRepository _shareRepository;
            private readonly IMapper _mapper;
            private readonly ShareBusinessRules _shareBusinessRules;

            public CreateShareCommandHandler(IShareRepository shareRepository, IMapper mapper, ShareBusinessRules shareBusinessRules)
            {
                _shareRepository = shareRepository;
                _mapper = mapper;
                _shareBusinessRules = shareBusinessRules;
            }

            public async Task<CreateShareDTO> Handle(CreateShareCommand request, CancellationToken cancellationToken)
            {
                await _shareBusinessRules.ShareNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _shareBusinessRules.ShareSymbolCanNotBeDuplicatedWhenInserted(request.Symbol);

                Share mappedShare = _mapper.Map<Share>(request);
                Share createdShare = await _shareRepository.AddAsync(mappedShare);
                CreateShareDTO createdShareDto = _mapper.Map<CreateShareDTO>(createdShare);

                return createdShareDto;
            }
        }
    }
}
