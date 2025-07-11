using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services.ContactFunctions.ContactExportFunctions;

public class ContactExportToCsvFunction : IContactExportFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ContactExportToCsvFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "2";
    public string Description => "Exports contacts to csv file";

    public async Task Action()
    {
        var contacts = await _db.GetAllAsync();
        await using (var writer = new StreamWriter("contacts.csv"))
        {
            await writer.WriteLineAsync("Name,Phone,Favorite"); // CSV header

            foreach (var contact in contacts)
            {
                // Escape commas if needed
                var name = contact.Name?.Replace(",", "");
                var phone = contact.Phone?.Replace(",", "");
                var favorite = contact.Favorite ? "Yes" : "No";

                await writer.WriteLineAsync($"{name},{phone},{favorite}");
            }
        }

        Console.WriteLine("Contacts exported to CSV file");
    }
}