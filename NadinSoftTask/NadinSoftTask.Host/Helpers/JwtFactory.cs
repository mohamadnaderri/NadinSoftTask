using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace NadinSoftTask.Host.Helpers
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateEncodedToken(IdentityUser user, IReadOnlyCollection<string> userRoles, IEnumerable<string> roleIds, ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(',', userRoles)));
            identity.AddClaim(new Claim("id", user.Id));
            identity.AddClaim(new Claim("userName", user.UserName));
            identity.AddClaim(new Claim("roleIds", string.Join(',', roleIds)));

            if (!string.IsNullOrEmpty(user.PhoneNumber))
                identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: identity.Claims,
                notBefore: _jwtOptions.NotBefore,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
    }
}
