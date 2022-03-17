using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IAsyncRepository : IRepository
{
    Task<IQueryable<T>> Select<T>(Expression<Func<T, bool>> filters) where T : class;
    Task<IQueryable<T>> SelectForTasks<T>(Expression<Func<T, bool>> filters) where T : class;

    Task InsertOne(object entity);
    Task InsertList(IReadOnlyList<object> entities);

    Task UpdateOne(object entity);
    Task UpdateList(IReadOnlyList<object> entities);

    Task DeleteOne(object entity);
    Task DeleteList(IReadOnlyList<object> entities);
}

