using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleQuery : IRequest<string>
{
    public int Id { get; set; }

}

public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, string>
{
    private readonly ITestService _testService;

    public GetAllSampleQueryHandler(ITestService testService)
    {
        _testService = testService;
    }

    public async Task<string> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        return $"{await _testService.GetStringAsync()} - passed id: {request.Id}";
    }
}

