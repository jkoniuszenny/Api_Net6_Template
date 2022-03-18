using MediatR;
using Shared.GlobalResponse;

namespace Application.CQRS.Sample.Commands.Add
{
    public class AddSampleCommand : IRequest<GlobalResponse<string>>
    {

    }
}
