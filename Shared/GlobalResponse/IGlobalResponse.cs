namespace Shared.GlobalResponse;

public interface IGlobalResponse<T>
{
    bool Succeded { get; set; }
    Payload<T>? Payload { get; set; }
    Error? Error { get; set; }
}

public interface IError
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? ValidationErrors { get; set; }
}

public interface IPayload<T>
{
    public T? Data { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public int? Total { get; set; }
}

public interface INullClass
{

}
