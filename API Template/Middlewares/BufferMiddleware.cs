namespace Api.Middlewares;

public class BufferMiddleware
{
    private readonly RequestDelegate _next;

    public BufferMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await HandleBufferEnabled(context);

        await _next(context);

    }

    private async Task HandleBufferEnabled(HttpContext context)
    {
        context.Request.EnableBuffering();

        await Task.CompletedTask;
    }

}
