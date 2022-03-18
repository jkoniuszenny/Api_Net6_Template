namespace Shared.GlobalResponse;

public class GlobalResponse<T> : IGlobalResponse<T>
{
    public bool Succeded { get; set; }
    public Payload<T>? Payload { get; set; }
    public Error? Error { get; set; }


    public static GlobalResponse<T> Fail()
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = false
        };

        return result;
    }

    public static GlobalResponse<T> Fail(string message)
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = false,
            Error = new Error() { Message = message }
        };

        return result;
    }

    public static GlobalResponse<T> Fail(int code, string message)
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = false,
            Error = new Error()
            {
                Code = code,
                Message = message
            }
        };

        return result;
    }

    public static GlobalResponse<T> Fail(int code, string message, IEnumerable<string> validations)
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = false,
            Error = new Error()
            {
                Code = code,
                Message = message,
                ValidationErrors = validations
            }
        };

        return result;
    }

    public static async Task<GlobalResponse<T>> FailAsync() => await Task.FromResult(Fail());

    public static async Task<GlobalResponse<T>> FailAsync(string message) => await Task.FromResult(Fail(message));

    public static async Task<GlobalResponse<T>> FailAsync(int code, string message) => await Task.FromResult(Fail(code, message));
    
    public static async Task<GlobalResponse<T>> FailAsync(int code, string message, IEnumerable<string> validations) => await Task.FromResult(Fail(code, message, validations));



    public static GlobalResponse<T> Success()
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = true
        };

        return result;
    }

    public static GlobalResponse<T> Success(T data)
    {
        var result = new GlobalResponse<T>()
        {
            Succeded = true,
            Payload = new Payload<T>() { Data = data }
        };
        
        return result;
    }

    public static async Task<GlobalResponse<T>> SuccessAsync() => await Task.FromResult(Success());

    public static async Task<GlobalResponse<T>> SuccessAsync(T data) => await Task.FromResult(Success(data));
}

public class Error
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? ValidationErrors { get; set; }
}

public class Payload<T> 
{
    public T? Data { get; set; }
}

public class NullClass
{

}

