using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class SearchContactsByNameFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public SearchContactsByNameFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "6";
    public string Description => "Search contact by name";

    public Task Action()
    {
        Console.Write("Please enter name to search: ");
        var name = Console.ReadLine() ?? string.Empty;
        var contacts = _db.GetQueryable().Where(c => c.Name.ToLower().Contains(name.ToLower()));
        foreach (var contact in contacts)
            Console.WriteLine($"{contact.Name}: {contact.Phone}. Favorite: {(contact.Favorite ? "Yes" : "No")}");
        return Task.CompletedTask;
    }
}