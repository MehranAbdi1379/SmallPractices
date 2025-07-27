using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services.ContactFunctions;

public class DisplayFavoriteContactsFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public DisplayFavoriteContactsFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "displayFavorite";
    public string Description => "Display favorite contacts";

    public async Task Action()
    {
        var contacts = _db.GetQueryable().Where(c => c.Favorite).ToList();

        if (contacts.Count == 0)
        {
            Console.WriteLine("There are no contacts");
            return;
        }

        foreach (var contact in contacts)
            Console.WriteLine($"{contact.Name}: {contact.Phone}.");
    }
}