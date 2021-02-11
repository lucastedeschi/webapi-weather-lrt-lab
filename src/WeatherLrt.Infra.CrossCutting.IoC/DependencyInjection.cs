using System;
using System.Net.Http;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using WeatherLrt.Application.Commands.ApplicationUsers.SignIn;
using WeatherLrt.Application.IdentityServices;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Infra.CrossCutting.Configuration.Settings;
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
            services.AddDbContext<WeatherLrtContext>(c => c.UseSqlServer(configuration.GetConnectionString(WeatherLrtSqlServerConnectionString)));
            services.AddScoped<IWeatherLrtContext>(c => c.GetRequiredService<WeatherLrtContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WeatherLrtContext>()
                .AddDefaultTokenProviders();

            services.AddMediatR(typeof(SignInApplicationUserCommand).Assembly);
            services.AddAutoMapper(typeof(ClothingItemProfile).Assembly);

            var openWeatherSettings = new OpenWeatherSettings();
            configuration.Bind(nameof(OpenWeatherSettings), openWeatherSettings);
            services.AddSingleton(openWeatherSettings);

            var tokenSettings = new TokenSettings();
            configuration.Bind(nameof(TokenSettings), tokenSettings);
            services.AddSingleton(tokenSettings);

            var signingSettings = new SigningSettings();
            services.AddSingleton(signingSettings);

            services.AddHttpClient(OpenWeatherCurrentClientName).AddPolicyHandler(GetRetryPolicy());

            services.AddScoped<IOpenWeatherServiceClient, OpenWeatherServiceClient>();
            services.AddScoped<IApplicationUserIdentityService, ApplicationUserIdentityService>();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingSettings.Key;
                paramsValidation.ValidAudience = tokenSettings.Audience;
                paramsValidation.ValidIssuer = tokenSettings.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
