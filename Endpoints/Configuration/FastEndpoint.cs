using Endpoints.Enum;

namespace Endpoints.Configuration;

public abstract class FastEndpoint : IFastEndpoint
{
    public HttpRequestMethodTypes Method { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Tag { get; set; }
    public bool Authorization { get; set; }
}