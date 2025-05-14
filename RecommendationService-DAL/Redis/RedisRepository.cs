using StackExchange.Redis;

namespace RecommendationService_DAL.Redis;

public class RedisRepository : IRedisRepository
{
    private readonly IDatabase _db;
    
    private const string ToDoTaskEventKeyPrefix = "taskEvent:";

    public RedisRepository(ConfigurationOptions configuration)
    {
        var redis = ConnectionMultiplexer.Connect(configuration);
        _db = redis.GetDatabase();
    }
}