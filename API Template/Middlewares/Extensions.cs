namespace API_Template.Middlewares;

public static class Extensions
{
    public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
    public static IApplicationBuilder ConfigureResponseTime(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware(typeof(ResponseTimeLoggerMiddleware));
    }
    public static IApplicationBuilder ConfigureBuffer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware(typeof(BufferMiddleware));
    }
    public static IApplicationBuilder UsernameLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware(typeof(UsernameLogMiddleware));
    }
    public static IApplicationBuilder RequestLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware(typeof(RequestLogMiddleware));
    }
}

