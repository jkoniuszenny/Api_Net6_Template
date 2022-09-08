using Application.CQRS.Sample.Commands.Add;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Authenticate;

public class SampleCommand : FastEndpoint
{
    public SampleCommand()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/SampleCommand/{id}";
        Name = "SampleCommand";
        Tag = "Sample";
    }


    public async Task<IResult> ExecuteAsync(IMediator mediator)
    {
        var result = await mediator.Send(new AddSampleCommand());

        return Results.Ok(result);
    }

}

