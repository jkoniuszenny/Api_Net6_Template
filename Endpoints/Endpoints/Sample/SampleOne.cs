using Application.CQRS.Sample.Queries.GetAll;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Endpoints.Endpoints.Sample;

public class SampleOne : FastEndpoint
{
    public SampleOne()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/sampleget/{id}";
        Name = "One";
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

