using Microsoft.AspNetCore.Mvc;

namespace Shared.GlobalResponse;

public class Response<T> : IResponse<T>
    where T : class
{
    public bool Succeded { get; set; }
    public Payload<T>? Payload { get; set; }
    public Error? Error { get; set; }


    public static JsonResult Fail()
    {
        var result = new Response<NullClass>()
        {
            Succeded = false
        };

        return new JsonResult(result) ;
    }

    public static JsonResult Fail(string message)
    {
        var result = new Response<NullClass>()
        {
            Succeded = false,
            Error = new Error() { Message = message }
        };

        return new JsonResult(result);
    }

    public static JsonResult Fail(int code, string message)
    {
        var result = new Response<NullClass>()
        {
            Succeded = false,
            Error = new Error()
            {
                Code = code,
                Message = message
            }
        };

        return new JsonResult(result);
    }

    public static async Task<JsonResult> FailAsync() => await Task.FromResult(Fail());

    public static async Task<JsonResult> FailAsync(string message) => await Task.FromResult(Fail(message));

    public static async Task<JsonResult> FailAsync(int code, string message) => await Task.FromResult(Fail(code, message));



    public static JsonResult Success()
    {
        var result = new Response<NullClass>()
        {
            Succeded = true
        };

        return new JsonResult(result);
    }

    public static JsonResult Success(T data)
    {
        var result = new Response<T>()
        {
            Succeded = true,
            Payload = new Payload<T>() { Data = data }
        };

        return new JsonResult(result);
    }

    public static async Task<JsonResult> SuccessAsync()=> await Task.FromResult(Success());

    public static async Task<JsonResult> SuccessAsync(T data) => await Task.FromResult(Success(data));
}

public class Error
{
    public int Code { get; set; }
    public string? Message { get; set; }
}

public class Payload<T> where T : class
{
    public T? Data { get; set; }
}

public class NullClass
{

}

