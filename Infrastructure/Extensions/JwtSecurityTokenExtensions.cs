using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Extensions;

public static class JwtSecurityTokenExtensions
{
    public static string GetClaims(this JwtSecurityToken token, string claimsName, string ifNotFoundReturn = "")
    {
        return token?.Claims?.FirstOrDefault(s => s.Type.ToLower() == claimsName.ToLower())?.Value ?? ifNotFoundReturn;
    }
}

