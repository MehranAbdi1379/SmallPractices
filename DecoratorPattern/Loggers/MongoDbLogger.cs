using MongoDB.Bson;
using MongoDB.Driver;

namespace DecoratorPattern.Loggers;

public class MongoDbLogger : ILogger
{
    private readonly ILogger _decorated;
    private readonly IMongoCollection<BsonDocument> _logs;

    public MongoDbLogger(string connectionString, ILogger decorated)
    {
        _decorated = decorated;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("LoggingPracticeDecoratorPattern");
        _logs = database.GetCollection<BsonDocument>("logs");
    }

    public void Log(string message)
    {
        var document = new BsonDocument
        {
            { "message", message },
            { "loggedAt", DateTime.UtcNow }
        };

        _logs.InsertOne(document);
        _decorated.Log(message);
    }
}