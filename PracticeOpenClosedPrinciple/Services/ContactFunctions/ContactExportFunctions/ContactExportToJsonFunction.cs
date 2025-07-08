using System.Text.Json;
using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services.ContactFunctions.ContactExportFunctions;

public class ContactExportToJsonFunction : IContactExportFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactExportToJsonFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "3";
    public string Description => "Export to JSON file";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true // Makes the JSON pretty
        };

        var jsonString = JsonSerializer.Serialize(contacts, options);
        await File.WriteAllTextAsync("contacts.json", jsonString);

        Console.WriteLine("Contacts exported to JSON file");
    }
}