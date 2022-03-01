using Endpoints.Enum;


namespace Endpoints.Configuration
{
    public interface IFastEndpoint
    {
        HttpRequestMethodTypes Method { get; set; }

        string? Url { get; set; }

        string? Tag { get; set; }

        string? Name { get; set; }

        bool Authorization { get; set; }
    }
}
