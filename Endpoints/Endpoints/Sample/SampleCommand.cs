using Application.CQRS.Sample.Commands.Add;
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

