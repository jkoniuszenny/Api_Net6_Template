using Application.Interfaces.Providers;
using Infrastructure.Database;
using MongoDB.Driver;
using Shared.Settings;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseMongoRepository
{
    protected readonly DatabaseMongoContext _databaseContext;
    protected readonly DatabaseMongoSettings _databaseSettings;
    private readonly IUserProvider _userProvider;

    public BaseMongoRepository(
        DatabaseMongoContext databaseContext,
        DatabaseMongoSettings databaseSettings,
        IUserProvider userProvider)
    {
        _databaseContext = databaseContext;
        _databaseSettings = databaseSettings;
        _userProvider = userProvider;
    }



    public async Task DeleteList<T>(IReadOnlyList<T> entities) where T : class
    {
        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);

        foreach (var item in entities)
        {
            var value = item!.GetType().GetProperty("Id")!.GetValue(item);

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", value);

            await collection.DeleteOneAsync(filter);
        }
    }

    public async Task DeleteOne<T>(T entity) where T : class
    {
        var value = typeof(T).GetProperty("Id")!.GetValue(entity);

        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", value);

        await collection.DeleteOneAsync(filter);
    }

    public async Task InsertList<T>(IReadOnlyList<T> entities) where T : class
    {
        foreach (var item in entities)
        {
            try
            {
                var createTmsTmp = item.GetType().GetProperty("CreateTmsTmp")!;
                createTmsTmp.SetValue(item, DateTime.UtcNow, null);

                var createUser = item.GetType().GetProperty("CreateUser")!;
                createUser.SetValue(item, _userProvider.UserName, null);
            }
            catch { }
        }


        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);
        await collection.InsertManyAsync(entities);
    }

    public async Task InsertOne<T>(T entity) where T : class
    {
        try
        {
            var createTmsTmp = entity.GetType().GetProperty("CreateTmsTmp")!;
            createTmsTmp.SetValue(entity, DateTime.UtcNow, null);

            var createUser = entity.GetType().GetProperty("CreateUser")!;
            createUser.SetValue(entity, _userProvider.UserName, null);
        }
        catch { }


        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);
        await collection.InsertOneAsync(entity);
    }

    public async Task<IReadOnlyList<T>> Select<T>(Expression<Func<T, bool>> filters) where T : class
    {
        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);

        FilterDefinition<T> filter = Builders<T>.Filter.Where(filters);

        return await collection.Find(filter).ToListAsync();
    }

    public async Task UpdateList<T>(IReadOnlyList<T> entities)
    {
        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);

        foreach (var item in entities)
        {
            try
            {
                var modifiedTmsTmp = item!.GetType().GetProperty("ModifiedTmsTmp")!;
                modifiedTmsTmp.SetValue(item, DateTime.UtcNow, null);

                var modifiedUser = item!.GetType().GetProperty("ModifiedUser")!;
                modifiedUser.SetValue(item, _userProvider.UserName, null);
            }
            catch { }

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", item!.GetType().GetProperty("Id")!.GetValue(item));

            await collection.ReplaceOneAsync(filter, item);
        }
    }

    public async Task UpdateOne<T>(T entity)
    {
        try
        {
            var modifiedTmsTmp = entity!.GetType().GetProperty("ModifiedTmsTmp")!;
            modifiedTmsTmp.SetValue(entity, DateTime.UtcNow, null);

            var modifiedUser = entity!.GetType().GetProperty("ModifiedUser")!;
            modifiedUser.SetValue(entity, _userProvider.UserName, null);
        }
        catch { }

        var collection = await _databaseContext.GetCollection<T>(typeof(T).Name);

        FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", entity!.GetType().GetProperty("Id")!.GetValue(entity));

        await collection.ReplaceOneAsync(filter, entity);

    }

}
