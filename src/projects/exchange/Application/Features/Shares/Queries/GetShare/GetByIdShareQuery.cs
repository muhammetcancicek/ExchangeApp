﻿using Application.Features.Shares.Dtos;
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

namespace Application.Features.Shares.Queries.GetShare
{
    public class GetByIdShareQuery : IRequest<ShareGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdShareQueryHandler : IRequestHandler<GetByIdShareQuery, ShareGetByIdDto>
        {
            private readonly IShareRepository _shareRepository;
            private readonly IMapper _mapper;
            private readonly ShareBusinessRules _shareBusinessRules;

            public GetByIdShareQueryHandler(IShareRepository shareRepository, IMapper mapper, ShareBusinessRules shareBusinessRules)
            {
                _shareRepository = shareRepository;
                _mapper = mapper;
                _shareBusinessRules = shareBusinessRules;
            }

            public async Task<ShareGetByIdDto> Handle(GetByIdShareQuery request, CancellationToken cancellationToken)
            {
                Share? share = await _shareRepository.GetAsync(b => b.Id == request.Id);
                _shareBusinessRules.ShareShouldExistWhenRequested(share);

                ShareGetByIdDto ShareGetByIdDto = _mapper.Map<ShareGetByIdDto>(share);
                return ShareGetByIdDto;
            }
        }
    }
}
