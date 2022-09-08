using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Settings;

namespace Infrastructure.Database;

public class DatabaseMongoContext
{
    private readonly DatabaseMongoSettings _settings;

    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public DatabaseMongoContext(
        DatabaseMongoSettings settings
        )
    {
        _settings = settings;

        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public async Task<IMongoCollection<T>> GetCollection<T>(string collectionName)
    {
        return await Task.FromResult(_database.GetCollection<T>(collectionName));
    }

    public async Task<bool> CheckHealthAsync()
    {
        try
        {
            await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
        }
        catch
        {
            return false;
        }

        return true;


    }
}
