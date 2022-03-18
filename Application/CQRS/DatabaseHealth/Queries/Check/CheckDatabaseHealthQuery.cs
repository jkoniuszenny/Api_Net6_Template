using MediatR;

namespace Application.CQRS.DatabaseHealth.Queries.Check;

public class CheckDatabaseHealthQuery : IRequest<string>
{
}

