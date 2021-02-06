using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Persistence;

namespace WeatherLrt.Infra.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateSystemUserCommand).Assembly);

            services.AddAutoMapper(typeof(SystemUserProfile).Assembly);

            services.AddDbContext<IWeatherLrtContext, WeatherLrtContext>(c => c.UseSqlServer("connectionString"), ServiceLifetime.Scoped);

            //services.AddScoped<IOpenWeatherServiceClient, OpenWeatherServiceClient>();
        }
    }
}
