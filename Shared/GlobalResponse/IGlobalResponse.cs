namespace Shared.GlobalResponse;

public interface IGlobalResponse<T>
{
    bool Succeded { get; set; }
    Payload<T>? Payload { get; set; }
    Error? Error { get; set; }
}

