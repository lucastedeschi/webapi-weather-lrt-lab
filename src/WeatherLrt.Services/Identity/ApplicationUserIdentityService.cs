using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Infra.CrossCutting.Configuration.Settings;

namespace WeatherLrt.Application.IdentityServices
{
    public sealed class ApplicationUserIdentityService : IApplicationUserIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenSettings _tokenSettings;
        private readonly SigningSettings _signingSettings;

        public ApplicationUserIdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, TokenSettings tokenSettings, SigningSettings signingSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenSettings = tokenSettings;
            _signingSettings = signingSettings;
        }

        public async Task<(bool signedIn, string token, string errorMessage)> SignIn(string email, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser is null)
                return (false, default, "Invalid email or password");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, false);
            if (!signInResult.Succeeded)
                return (false, default, "Invalid email or password");

            var identity = new ClaimsIdentity(
                new GenericIdentity(email, "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, email)
                }
            );

            var handler = new JwtSecurityTokenHandler();

            var createdDate = DateTime.Now;
            var expirationDate = createdDate + TimeSpan.FromSeconds(_tokenSettings.Seconds);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = _signingSettings.SigningCredentials,
                Subject = identity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return (true, token, default);
        }

        public async Task<(bool signedUp, string applicationUserId, string errorMessage)> SignUp(string userName, string email, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser != null)
                return (false, default, $"Email ({email}) already exists");

            applicationUser = await _userManager.FindByNameAsync(userName);
            if (applicationUser != null)
                return (false, default, $"UserName ({userName}) already exists");

            applicationUser = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, password);
            if (!identityResult.Succeeded)
                return (false, default, identityResult.Errors.FirstOrDefault()?.Description);

            return (true, applicationUser.Id, default);
        }
    }
}
