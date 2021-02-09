using System;
using System.Net.Http;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Infra.CrossCutting.Configuration.Options;
using WeatherLrt.Persistence;
using WeatherLrt.Services.OpenWeather;

namespace WeatherLrt.Infra.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        private const string OpenWeatherCurrentClientName = "OpenWeatherCurrentClientName";
        private const string WeatherLrtSqlServerConnectionString = "WeatherLrtSqlServer";

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(CreateSystemUserCommand).Assembly);
            services.AddAutoMapper(typeof(SystemUserProfile).Assembly);

            var sqlServerConnectionString = configuration.GetConnectionString(WeatherLrtSqlServerConnectionString);

            services.AddDbContext<IWeatherLrtContext, WeatherLrtContext>(c => c.UseSqlServer(sqlServerConnectionString), ServiceLifetime.Scoped);

            services.AddHttpClient(OpenWeatherCurrentClientName)
                .AddPolicyHandler(GetRetryPolicy());

            var openWeatherOptions = new OpenWeatherOptions();
            configuration.Bind(nameof(OpenWeatherOptions), openWeatherOptions);
            services.AddSingleton(openWeatherOptions);

            services.AddScoped<IOpenWeatherServiceClient, OpenWeatherServiceClient>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
