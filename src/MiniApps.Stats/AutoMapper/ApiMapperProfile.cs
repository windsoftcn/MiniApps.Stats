using AutoMapper;
using MiniApps.Stats.Api;
using MiniApps.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.AutoMapper
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.CreateTime, opt => opt.Ignore());
        }
    }
}
