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

            if (!Guid.TryParse(GetClaim(claims, "tokenId"), out var result))
            {
                return null;
            }

            if (!Guid.TryParse(GetClaim(claims, "userId"), out var result2))
            {
                return null;
            }

            long.TryParse(GetClaim(claims, "tokenExp"), out var result3);
            DateTime tokenExpireAt = new DateTime(result3);
            long.TryParse(GetClaim(claims, "refreshExp"), out var result4);
            DateTime refreshExpireAt = new DateTime(result4);
            UserInfo userInfo = new UserInfo();
            userInfo.UserId = result2;
            userInfo.TokenId = result;
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
