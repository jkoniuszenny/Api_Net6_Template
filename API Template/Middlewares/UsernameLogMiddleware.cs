using Application.Interfaces.Providers;

namespace API_Template.Middlewares;

public class UsernameLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserProvider _userProvider;

    public UsernameLogMiddleware(RequestDelegate next, IUserProvider userProvider)
    {
        _next = next;
        _userProvider = userProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        await UsernameLogAddTo(context);

        await _next(context);

    }

    private async Task UsernameLogAddTo(HttpContext context)
    {
        context.Items["userNameKey"] = _userProvider.UserName;

        await Task.CompletedTask;
    }
}
