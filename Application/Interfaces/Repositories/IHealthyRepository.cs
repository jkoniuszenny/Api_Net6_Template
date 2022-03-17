
namespace Application.Interfaces.Repositories;

public interface IHealthyRepository : IRepository
{
    Task<string> GetHealthyAsync();
}

