using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NadinSoftTask.Host.Helpers
{
    public class TokenGenerator
    {
        public static JsonWebToken EmptyToken = new JsonWebToken();
        public static JsonWebToken GenerateJwt(IdentityUser user, IReadOnlyCollection<string> userRoles, IReadOnlyCollection<string> userRoleIds, ClaimsIdentity identity,
            IJwtFactory jwtFactory, JsonSerializerSettings serializerSettings)
        {
            var response = new JsonWebToken
            {
                userName = user.UserName,
                token_type = "Bearer",
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = jwtFactory.GenerateEncodedToken(user, userRoles, userRoleIds, identity),
            };
            return response;
        }
    }
}
