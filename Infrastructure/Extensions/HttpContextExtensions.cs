using Microsoft.AspNetCore.Http;


namespace Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static string GetToken(this IHttpContextAccessor httpContent)
    {
        string token =
           httpContent?.HttpContext?.Request?.Headers?.FirstOrDefault(f => f.Key.ToLower() == "authorization").Value
           .ToString()?
           .Replace("Bearer ", "") ?? string.Empty;

        return token;

    }
}

