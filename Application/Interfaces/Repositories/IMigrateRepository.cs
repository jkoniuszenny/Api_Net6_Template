
namespace Application.Interfaces.Repositories;

public interface IMigrateRepository : IRepository
{
    Task MigrateExecute();
}

