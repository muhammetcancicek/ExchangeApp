
using Application.Features.Shares.Commands.CreateShare;
using Application.Features.Shares.Commands.EditShare;
using Application.Features.Shares.Dtos;
using Application.Features.Shares.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<Share>, ShareListModel>().ReverseMap();
            CreateMap<IPaginate<ShareListDtoWithPrice>, ShareListModel>().ReverseMap();
            CreateMap<Share,ShareListDto>().ReverseMap();
            CreateMap<Share, ShareGetByIdDto>().ReverseMap();
            CreateMap<Share, ShareGetByIdDto>().ReverseMap();
            CreateMap<Share, CreateShareDTO>().ReverseMap();
            CreateMap<Share, CreateShareCommand>().ReverseMap();
            CreateMap<Share, EditShareDTO>().ReverseMap();
            CreateMap<Share, EditShareCommand>().ReverseMap();
        }
    }
}
