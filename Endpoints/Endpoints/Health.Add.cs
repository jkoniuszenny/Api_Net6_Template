using Application.Interfaces.Services;
using Endpoints.Configuration;
using Endpoints.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints;

public class HealthAdd : FastEndpoint
{
    public HealthAdd()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/testPost";
        Authorization = true;
    }

    [Authorize]
    public async Task<JsonResult> ExecuteAsync(ITestService test)
    {
       return new JsonResult(await test.GetStringAsync());
    }
}

