using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services.ContactFunctions.ContactExportFunctions;

public class ContactExportToTextFunction : IContactExportFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactExportToTextFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "1";
    public string Description => "Exports contacts into text file";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        await File.WriteAllLinesAsync("contacts.txt",
            contacts.Select(c => $"{c.Name}: {c.Phone}, Favorite: {(c.Favorite ? "Yes" : "No")}"));
        Console.WriteLine("Contacts have been exported to text file");
    }
}