using System;
using System.Net.Http;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Persistence;
using WeatherLrt.Services.OpenWeather;

namespace WeatherLrt.Infra.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        private const string OpenWeatherCurrentClientName = "OpenWeatherCurrentClientName";

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateSystemUserCommand).Assembly);

            services.AddAutoMapper(typeof(SystemUserProfile).Assembly);

            services.AddDbContext<IWeatherLrtContext, WeatherLrtContext>(c => c.UseSqlServer("connectionString"), ServiceLifetime.Scoped);

            services.AddHttpClient(OpenWeatherCurrentClientName)
                .AddPolicyHandler(GetRetryPolicy());

            services.AddScoped<IOpenWeatherServiceClient, OpenWeatherServiceClient>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
