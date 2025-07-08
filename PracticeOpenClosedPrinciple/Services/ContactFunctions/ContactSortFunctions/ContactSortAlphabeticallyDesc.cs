using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple;

public class ContactSortAlphabeticallyDesc : IContactSortFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactSortAlphabeticallyDesc(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "2";
    public string Description => "Sort by name (Z-A)";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        contacts = contacts.OrderByDescending(c => c.Name).ToList();

        foreach (var contact in contacts) Console.WriteLine($"{contact.Name}: {contact.Phone}");
    }
}