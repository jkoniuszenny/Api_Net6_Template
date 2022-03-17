using Application.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Settings;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository : IAsyncRepository
{
    protected readonly DatabaseContext _databaseContext;
    protected readonly DatabaseSettings _databaseSettings;

    public BaseRepository(
        DatabaseContext databaseContext,
        DatabaseSettings databaseSettings)
    {
        _databaseContext = databaseContext;
        _databaseSettings = databaseSettings;
    }

    public async Task DeleteList(IReadOnlyList<object> entities)
    {
        _databaseContext.RemoveRange(entities);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteOne(object entity)
    {
        _databaseContext.Remove(entity);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task InsertList(IReadOnlyList<object> entities)
    {
        try
        {
            await _databaseContext.AddRangeAsync(entities);
            await _databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
        }
        finally
        {
            Parallel.ForEach(entities, p =>
            {
                _databaseContext.Entry(p).State = EntityState.Detached;
            });
        }
    }

    public async Task InsertOne(object entity)
    {
        try
        {
            await _databaseContext.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
        }
        finally
        {
            _databaseContext.Entry(entity).State = EntityState.Detached;
        }
    }

    public async Task<IQueryable<T>> Select<T>(Expression<Func<T, bool>> filters) where T : class
    {
        return await Task.FromResult(_databaseContext.Set<T>()
            .Where(filters)
            .AsNoTracking()
            .AsQueryable());
    }

    public async Task<IQueryable<T>> SelectForTasks<T>(Expression<Func<T, bool>> filters) where T : class
    {
        DatabaseContext context = new DatabaseContext(_databaseContext.Options, _databaseSettings);

        return await Task.FromResult(context.Set<T>()
                .Where(filters)
                .AsNoTracking()
                .AsQueryable());
    }

    public async Task UpdateList(IReadOnlyList<object> entities)
    {
        try
        {
            _databaseContext.UpdateRange(entities);
            await _databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
        }
        finally
        {
            Parallel.ForEach(entities, p =>
            {
                _databaseContext.Entry(p).State = EntityState.Detached;
            });
        }
    }

    public async Task UpdateOne(object entity)
    {
        try
        {
            _databaseContext.Update(entity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
        }
        finally
        {

            _databaseContext.Entry(entity).State = EntityState.Detached;

        }
    }
}
