using Application.CQRS.Sample.Commands.Add;
using Application.Interfaces.Services;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints;

public class State : FastEndpoint
{
    public State()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/command/{id}";
        Name = "Wooooo";
        Tag = "Mediator";
    }

    public async Task ExecuteAsync([FromServices] IMediator mediator, [FromQuery]int id)
    {
        await mediator.Send(new AddSampleCommand());

        await Task.CompletedTask;
    }

}

