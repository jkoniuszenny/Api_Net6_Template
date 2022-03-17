using MediatR;
using Shared.NLog.Interfaces;

namespace Application.CQRS.Sample.Commands.Add
{
    public class AddSampleCommand : IRequest
    {
       
    }

    public class DeleteProductCommandHandler : IRequestHandler<AddSampleCommand>
    {
        private readonly INLogLogger _logger;

        public DeleteProductCommandHandler(INLogLogger logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(AddSampleCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return Unit.Value;
        }
    }
}
