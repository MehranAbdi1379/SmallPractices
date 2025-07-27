using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class DisplayAllContactsFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public DisplayAllContactsFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "display";
    public string Description => "Display all contacts";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        if (contacts.Count == 0)
        {
            Console.WriteLine("There are no contacts");
            return;
        }

        ;
        foreach (var contact in contacts)
            Console.WriteLine($"{contact.Name}: {contact.Phone}. Favorite: {(contact.Favorite ? "Yes" : "No")}");
    }
}