using Application.CQRS.Sample.Commands.Add;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints.Sample;

public class SampleTwo : FastEndpoint
{
    public SampleTwo()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/samplePost/{id}";
        Name = "Two";
        Tag = "Sample";
    }

    public async Task ExecuteAsync([FromServices] IMediator mediator, [FromQuery]int id)
    {
        await mediator.Send(new AddSampleCommand());

        await Task.CompletedTask;
    }

}

