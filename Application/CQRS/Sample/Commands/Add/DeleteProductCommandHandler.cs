using Shared.NLog.Interfaces;

namespace Application.CQRS.Sample.Commands.Add
{
    public class DeleteProductCommandHandler : IRequestHandler<AddSampleCommand, GlobalResponse<string>>
    {
        private readonly INLogLogger _logger;

        public DeleteProductCommandHandler(INLogLogger logger)
        {
            _logger = logger;
        }

        public async Task<GlobalResponse<string>> Handle(AddSampleCommand request, CancellationToken cancellationToken)
        {
            return await GlobalResponse<string>.SuccessAsync();
        }
    }
}
