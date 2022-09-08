using System.Text;

namespace API_Template.Middlewares;

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

        await _next(context).ConfigureAwait(false);

    }

    private async Task HandleBufferEnabled(HttpContext context)
    {
        context.Request.EnableBuffering();

        var requestBody = await GetString(context.Request.Body).ConfigureAwait(false);

        if (requestBody.Length > 0)
        {
            context.Items["bodyKey"] = requestBody;
        }

    }

    private async Task<string> GetString(Stream stream)
    {
        var originalPosition = stream.Position;

        stream.Position = 0;

        string responseText = null!;

        using (var streamReader = new StreamReader(
                          stream,
                          Encoding.UTF8,
                          true,
                          1024,
                          leaveOpen: true))
        {
            responseText = await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }

        stream.Position = originalPosition;

        return string.Concat(responseText.Where(c => !char.IsWhiteSpace(c)));
    }

}
