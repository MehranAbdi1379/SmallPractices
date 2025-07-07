using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Infrastructure;

public static class MongoClassMapping
{
    private static bool _initialized;

    public static void RegisterClassMaps()
    {
        if (_initialized) return;

        if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String)) // store GUIDs as strings (safer for querying)
                    .SetIdGenerator(GuidGenerator.Instance);
            });

        if (!BsonClassMap.IsClassMapRegistered(typeof(Contact)))
            BsonClassMap.RegisterClassMap<Contact>(cm => { cm.AutoMap(); });

        _initialized = true;
    }
}