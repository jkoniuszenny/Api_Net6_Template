using Application.Interfaces.Repositories;

namespace Application.CQRS.DatabaseHealth.Queries.CheckDatabaseHealth;

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
