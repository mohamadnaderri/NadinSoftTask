using System.Security.Claims;

namespace NadinSoftTask.Infrastructure
{
    public static class ClaimsExtensions
    {
        public static UserInfo ToUserInfo(this IList<Claim> claims)
        {
            if (claims == null || !claims.Any())
            {
                return null;
            }

            if (!Guid.TryParse(GetClaim(claims, "id"), out var userId))
            {
                return null;
            }

            long.TryParse(GetClaim(claims, "tokenExp"), out var tokenExp);
            DateTime tokenExpireAt = new DateTime(tokenExp);
            long.TryParse(GetClaim(claims, "refreshExp"), out var refreshExp);
            DateTime refreshExpireAt = new DateTime(refreshExp);
            UserInfo userInfo = new UserInfo();
            userInfo.UserId = userId;
            userInfo.Name = GetClaim(claims, "givenName");
            userInfo.Roles = (GetClaim(claims, "userRoles") ?? "").Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            userInfo.PersonType = GetClaim(claims, "personType");
            userInfo.PhoneNumber = GetClaim(claims, "phoneNumber");
            userInfo.UserName = GetClaim(claims, "userName");
            userInfo.TokenExpireAt = tokenExpireAt;
            userInfo.RefreshExpireAt = refreshExpireAt;
            return userInfo;
        }

        private static string GetClaim(IList<Claim> claims, string type)
        {
            return claims.FirstOrDefault((Claim i) => i.Type == type)?.Value;
        }
    }
}
