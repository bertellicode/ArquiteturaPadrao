using System.Text;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ArquiteturaPadrao.Infra.CrossCutting.JWT.Configurations
{
    public class CredentialsConfiguration : ICredentialsConfiguration
    {
        private const string SecretKeySigningCredentials = "SecretKeySignature";

        private const string SecretKeyEncryptingCredentials = "ProEMLh5x_qnzdNU";

        public SymmetricSecurityKey SymmetricKeySigningCredentials => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKeySigningCredentials));

        public SymmetricSecurityKey SymmetricKeyEncryptingCredentials => new SymmetricSecurityKey(Encoding.Default.GetBytes(SecretKeyEncryptingCredentials));

        public SigningCredentials SigningCredentials { get; }

        public EncryptingCredentials EncryptingCredentials { get; }

        public CredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(SymmetricKeySigningCredentials, SecurityAlgorithms.HmacSha256);

            EncryptingCredentials = new EncryptingCredentials(SymmetricKeyEncryptingCredentials,
                                    SecurityAlgorithms.Aes128KW,
                                    SecurityAlgorithms.Aes128CbcHmacSha256);
        }
    }
}
