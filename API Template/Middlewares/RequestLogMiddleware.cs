using Shared.NLog.Interfaces;
using System.Diagnostics;

namespace API_Template.Middlewares;

public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly INLogLogger _nLogLogger;

    public RequestLogMiddleware(RequestDelegate next, INLogLogger nLogLogger)
    {
        _next = next;
        _nLogLogger = nLogLogger;
    }

    public async Task Invoke(HttpContext context)
    {

        if (context.Request.Path.Value == "/"
            || context.Request.Path.Value?.ToLower() == "/health/api")
            await _next(context);
        else
        {
            var guid = Guid.NewGuid();

            var timer = new Stopwatch();

            _nLogLogger.LogInfo($"Request: {guid} start");

            timer.Start();
            await _next(context);
            timer.Stop();

            _nLogLogger.LogInfo($"Request: {guid} end and took {timer.Elapsed.Milliseconds / 1000.0}s.");
        }

    }
}
