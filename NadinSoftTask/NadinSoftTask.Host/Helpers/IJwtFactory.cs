using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NadinSoftTask.Host.Helpers
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(IdentityUser user, IReadOnlyCollection<string> userRoles, IEnumerable<string> roleIds, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
