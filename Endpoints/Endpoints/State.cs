using Application.CQRS.Sample.Commands.Add;
using Application.Interfaces.Services;
using Endpoints.Configuration;
using Endpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints;

public class State : FastEndpoint
{
    public State()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/command";
        Name = "Wooooo";
        Tag = "Mediator";
    }

    public async Task ExecuteAsync([FromServices] IMediator mediator)
    {
        await mediator.Send(new AddSampleCommand());

        await Task.CompletedTask;
    }

}

