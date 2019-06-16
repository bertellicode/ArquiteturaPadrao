using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces
{
    public interface ITokenConfiguration
    {
        SymmetricSecurityKey SymmetricKeySigningCredentials { get; }

        SymmetricSecurityKey SymmetricKeyEncryptingCredentials { get; }

        string Audience { get; set; }

        string Issuer { get; set; }

        int MinutesValid { get; set; }

        string Bearer { get; set; }

        Task<string> GenerateToken(ClaimsIdentity identityClaims);
    }
}
