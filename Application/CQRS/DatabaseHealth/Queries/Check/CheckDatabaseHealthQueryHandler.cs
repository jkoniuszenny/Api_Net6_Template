using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.DatabaseHealth.Queries.Check;

public class CheckDatabaseHealthQueryHandler : IRequestHandler<CheckDatabaseHealthQuery, string>
{
    private readonly IHealthyRepository _repository;

    public CheckDatabaseHealthQueryHandler(
        IHealthyRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CheckDatabaseHealthQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetHealthyAsync();
    }
}
