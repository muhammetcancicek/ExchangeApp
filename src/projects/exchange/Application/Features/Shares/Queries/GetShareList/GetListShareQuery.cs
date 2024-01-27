using Application.Features.Shares.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Queries.GetListShare
{
    public class GetListShareQuery:IRequest<ShareListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListShareQueryHandler : IRequestHandler<GetListShareQuery, ShareListModel>
        {
            private readonly IShareRepository _shareRepository;
            private readonly IMapper _mapper;

            public GetListShareQueryHandler(IShareRepository shareRepository, IMapper mapper)
            {
                _shareRepository = shareRepository;
                _mapper = mapper;
            }

            public async Task<ShareListModel> Handle(GetListShareQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Share> shares = await _shareRepository.GetListAsync(index: request.PageRequest.Page,size:request.PageRequest.PageSize);

                ShareListModel mappedShareListModel = _mapper.Map<ShareListModel>(shares);

                return mappedShareListModel;
            }
        }
    }
}
