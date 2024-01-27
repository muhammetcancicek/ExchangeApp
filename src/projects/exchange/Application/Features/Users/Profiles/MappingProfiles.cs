
using Application.Features.Shares.Dtos;
using Application.Features.Shares.Models;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.EditUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<User, UserGetByIdDto>().ReverseMap();
            CreateMap<User, UserGetByIdDto>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, EditUserDTO>().ReverseMap();
            CreateMap<User, EditUserCommand>().ReverseMap();
        }
    }
}
