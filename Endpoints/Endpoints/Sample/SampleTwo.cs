using Application.CQRS.Sample.Commands.Add;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Endpoints.Endpoints.Sample;

public class SampleTwo : FastEndpoint
{
    public SampleTwo()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/samplePost";
        Name = "Two";
        Tag = "Sample";
    }

    public async Task<IResult> ExecuteAsync(IMediator mediator)
    {
        var result = await mediator.Send(new AddSampleCommand());

        return Results.Ok(result);
    }

}

