using Shared.NLog.Interfaces;
using Shared.Settings;
using System.Diagnostics;

namespace API_Template.Middlewares;

public class ResponseTimeLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly INLogTimeLogger _logger;
    private readonly ResponseTimeSettings _settings;
    public ResponseTimeLoggerMiddleware(RequestDelegate next, ResponseTimeSettings settings, INLogTimeLogger logger)
    {
        _next = next;
        _settings = settings;
        _logger = logger;
    }
    public Task InvokeAsync(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();
        context.Response.OnStarting(() =>
        {
            watch.Stop();
            var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

            if (responseTimeForCompleteRequest > _settings.MilisecondsElapsedToNotify)
            {
                _logger.LogWarn($"Long request time { responseTimeForCompleteRequest / 1000.0}s.");
            }
            return Task.CompletedTask;
        });

        return _next(context);
    }
}

