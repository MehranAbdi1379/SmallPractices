using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple;

public class ContactSortAlphabeticallyAsc : IContactSortFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactSortAlphabeticallyAsc(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "1";
    public string Description => "Sort by name (A-Z)";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        contacts = contacts.OrderBy(c => c.Name).ToList();

        foreach (var contact in contacts)
            Console.WriteLine($"{contact.Name}: {contact.Phone}. Favorite: {(contact.Favorite ? "Yes" : "No")}");
    }
}