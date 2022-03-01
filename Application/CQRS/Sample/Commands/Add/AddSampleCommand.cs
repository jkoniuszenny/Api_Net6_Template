using Application.Interfaces.Services;
using Application.NLog.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Sample.Commands.Add
{
    public class AddSampleCommand : IRequest
    {
       
    }

    public class DeleteProductCommandHandler : IRequestHandler<AddSampleCommand>
    {
        private readonly ITestService _test;
        private readonly INLogLogger _logger;

        public DeleteProductCommandHandler(ITestService test, INLogLogger logger)
        {
            _test = test;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddSampleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo(await _test.GetStringAsync());
            await Task.CompletedTask;

            return Unit.Value;
        }
    }
}
