using Application.Interfaces.Providers;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Providers;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContext;

    public UserProvider(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string UserName
    {
        get
        {
            return GetJwtSecurityToken().GetClaims("username", "Missing username");
        }
    }

    private JwtSecurityToken GetJwtSecurityToken()
    {
        string token = _httpContext.GetToken();

        return token.Length == 0 ? new JwtSecurityToken() : new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}

