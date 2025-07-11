using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple;

public class ContactSortPhoneNumberLength : IContactSortFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactSortPhoneNumberLength(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "3";
    public string Description => "Sort by phone number length (increasing)";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        contacts = contacts.OrderBy(c => c.Phone.Length).ToList();

        foreach (var contact in contacts)
            Console.WriteLine($"{contact.Name}: {contact.Phone}. Favorite: {(contact.Favorite ? "Yes" : "No")}");
    }
}