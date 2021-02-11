using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace WeatherLrt.Infra.CrossCutting.Configuration.Settings
{
    public sealed class SigningSettings
    {
        public SigningSettings()
        {
            using var provider = new RSACryptoServiceProvider(2048);

            Key = new RsaSecurityKey(provider.ExportParameters(true));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public SecurityKey Key { get; }

        public SigningCredentials SigningCredentials { get; }
    }
}