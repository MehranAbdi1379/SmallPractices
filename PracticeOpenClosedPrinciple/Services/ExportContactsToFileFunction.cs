using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class ExportContactsToFileFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public ExportContactsToFileFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "5";
    public string Description => "Exports contacts to file";

    public async Task Action()
    {
        await File.WriteAllLinesAsync("contacts.txt", (await _db.GetAllAsync()).Select(c => $"{c.Name}: {c.Phone}"));
        Console.WriteLine("Contacts have been exported to file");
    }
}