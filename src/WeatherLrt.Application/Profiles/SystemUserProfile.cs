using AutoMapper;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Profiles
{
    public sealed class SystemUserProfile : Profile
    {
        public SystemUserProfile()
        {
            CreateMap<SystemUser, SystemUserResponse>();
        }
    }
}
