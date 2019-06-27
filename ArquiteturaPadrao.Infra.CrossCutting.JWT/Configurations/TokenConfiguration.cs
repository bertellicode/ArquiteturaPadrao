using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using ArquiteturaPadrao.Infra.CrossCutting.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ArquiteturaPadrao.Infra.CrossCutting.JWT.Configurations
{
    public class TokenConfiguration : ITokenConfiguration
    {
        private readonly ICredentialsConfiguration _credentialsConfiguration;

        private readonly IPathProvider _pathProvider;

        public SymmetricSecurityKey SymmetricKeySigningCredentials => _credentialsConfiguration.SymmetricKeySigningCredentials;

        public SymmetricSecurityKey SymmetricKeyEncryptingCredentials => _credentialsConfiguration.SymmetricKeyEncryptingCredentials;

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int MinutesValid { get; set; }

        public string Bearer { get; set; }

        public TokenConfiguration(ICredentialsConfiguration credentialsConfiguration,
                                    IPathProvider pathProvider)
        {
            _credentialsConfiguration = credentialsConfiguration;
            _pathProvider = pathProvider;
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "..", "ArquiteturaPadrao.Infra.CrossCutting.JWT");

            var filePath = _pathProvider.MapPathFromContentRoot("..", "ArquiteturaPadrao.Infra.CrossCutting.JWT");

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(filePath)
                                .AddJsonFile("appsettings.json")
                                .Build();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    configuration.GetSection("TokenConfiguration"))
                .Configure(this);
        }

        public async Task<string> GenerateToken(ClaimsIdentity identityClaims)
        {
            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = _credentialsConfiguration.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(MinutesValid),
                EncryptingCredentials = _credentialsConfiguration.EncryptingCredentials
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return encodedJwt;
        }
    }
}
