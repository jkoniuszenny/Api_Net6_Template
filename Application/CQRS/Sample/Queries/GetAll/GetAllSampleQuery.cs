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

    public GetAllSampleQueryHandler()
    {
    }

    public async Task<string> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult($"passed id: {request.Id}");
    }
}

