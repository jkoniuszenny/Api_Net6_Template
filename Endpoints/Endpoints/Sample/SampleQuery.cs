using Application.CQRS.Sample.Queries.GetAll;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Sample;

public class SampleQuery : FastEndpoint
{
    public SampleQuery()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/SampleQuery/{id}";
        Name = "SampleQuery";
        Tag = "Sample";
    }


    public async Task<IResult> ExecuteAsync(IMediator mediator, int id)
    {
        var result = await mediator.Send(new GetAllSampleQuery()
        {
            Id = id
        });

        return Results.Ok(result);
    }

}

