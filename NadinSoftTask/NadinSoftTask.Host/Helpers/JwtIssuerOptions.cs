using Microsoft.IdentityModel.Tokens;

namespace NadinSoftTask.Host.Helpers
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime NotBefore => DateTime.UtcNow;

        public DateTime IssuedAt => DateTime.UtcNow;

        public string SecretKey { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}