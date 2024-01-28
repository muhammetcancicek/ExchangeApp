
using Application.Features.Portfolios.Commands.CreatePortfolio;
using Application.Features.Portfolios.Commands.EditPortfolio;
using Application.Features.Portfolios.Dtos;
using Application.Features.Portfolios.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<Portfolio>, PortfolioListModel>().ReverseMap();
            CreateMap<Portfolio,PortfolioListDto>().ReverseMap();
            CreateMap<Portfolio, PortfolioGetByIdDto>().ReverseMap();
            CreateMap<Portfolio, PortfolioGetByIdDto>().ReverseMap();
            CreateMap<Portfolio, CreatePortfolioDTO>().ReverseMap();
            CreateMap<Portfolio, CreatePortfolioCommand>().ReverseMap();
            CreateMap<Portfolio, EditPortfolioDTO>().ReverseMap();
            CreateMap<Portfolio, EditPortfolioCommand>().ReverseMap();
        }
    }
}
