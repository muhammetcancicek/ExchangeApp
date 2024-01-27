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

namespace Application.Features.Shares.Commands.EditShare
{
    public class EditShareCommand : IRequest<EditShareDTO>
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public class EditShareCommandHandler : IRequestHandler<EditShareCommand, EditShareDTO>
        {
            private readonly IShareRepository _shareRepository;
            private readonly IMapper _mapper;
            private readonly ShareBusinessRules _shareBusinessRules;

            public EditShareCommandHandler(IShareRepository shareRepository, IMapper mapper, ShareBusinessRules shareBusinessRules)
            {
                _shareRepository = shareRepository;
                _mapper = mapper;
                _shareBusinessRules = shareBusinessRules;
            }

            public async Task<EditShareDTO> Handle(EditShareCommand request, CancellationToken cancellationToken)
            {
                Share? existingShare = await _shareRepository.GetAsync(b => b.Id == request.Id);
                _shareBusinessRules.ShareShouldExistWhenRequested(existingShare);

                await _shareBusinessRules.ShareNameCanNotBeDuplicatedWhenEdit(request.Name, request.Id);
                await _shareBusinessRules.ShareSymbolCanNotBeDuplicatedWhenEdit(request.Symbol, request.Id);

                _mapper.Map(request, existingShare);
                Share updatedShare = await _shareRepository.UpdateAsync(existingShare);

                EditShareDTO editedShareDto = _mapper.Map<EditShareDTO>(updatedShare);

                return editedShareDto;
            }
        }
    }
}
