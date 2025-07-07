using MongoDB.Driver;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Infrastructure;

public class MongoDbContext<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbContext()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("ContactAppDb");
        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await _collection.ReplaceOneAsync(a => a.Id == entity.Id, entity);
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        await _collection.DeleteOneAsync(a => a.Id == entity.Id);
        return entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return (await _collection.FindAsync(_ => true)).ToList();
    }
}