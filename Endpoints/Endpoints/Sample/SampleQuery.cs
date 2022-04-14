using Application.CQRS.Sample.Queries.GetAll;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointsController.Endpoints.Authenticate;

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

