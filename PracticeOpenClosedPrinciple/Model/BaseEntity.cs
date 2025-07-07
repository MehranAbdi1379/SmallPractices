using MongoDB.Bson;

namespace PracticeOpenClosedPrinciple.Model;

public class BaseEntity
{
    public ObjectId Id { get; set; }
}