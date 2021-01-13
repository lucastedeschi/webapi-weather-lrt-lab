using AutoMapper;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Profiles
{
    public sealed class SystemUserProfile : Profile
    {
        public void Map(Profile profile)
        {
            profile.CreateMap<SystemUser, SystemUserResponse>();
        }
    }
}
