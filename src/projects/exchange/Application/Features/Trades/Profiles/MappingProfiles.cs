
using Application.Features.Trades.Commands.CreateTrade;
using Application.Features.Trades.Dtos;
using Application.Features.Trades.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<Trade>, TradeListModel>().ReverseMap();
            CreateMap<Trade, TradeListDto>()
                .ForMember(dto => dto.PortfolioTitle, opt => opt.MapFrom(trade => trade.Portfolio.Title))
                .ForMember(dto => dto.ShareName, opt => opt.MapFrom(trade => trade.Share.Name))
                .ReverseMap();
            CreateMap<Trade, TradeGetByIdDto>()
                .ForMember(dto => dto.PortfolioTitle, opt => opt.MapFrom(trade => trade.Portfolio.Title))
                .ForMember(dto => dto.ShareName, opt => opt.MapFrom(trade => trade.Share.Name))
                .ReverseMap();

            CreateMap<Trade, BuyAndSellShareDTO>().ReverseMap();


            CreateMap<Trade, BuyAndSellShareDTO>().ReverseMap();
            CreateMap<Trade, BuyAndSellShareDTO>().ReverseMap();

        }
    }
}
