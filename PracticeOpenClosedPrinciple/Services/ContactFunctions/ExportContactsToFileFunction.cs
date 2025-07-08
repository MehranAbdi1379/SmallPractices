using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;
using PracticeOpenClosedPrinciple.Services.ContactFunctions.ContactExportFunctions;

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
        var exportFunctions = new List<IContactExportFunction>
        {
            new ContactExportToTextFunction(_db),
            new ContactExportToCsvFunction(_db),
            new ContactExportToJsonFunction(_db)
        };

        while (true)
        {
            foreach (var function in exportFunctions)
                Console.WriteLine($"{function.OptionCode}: {function.Description}");

            Console.Write("Please enter sort option: ");
            var option = Console.ReadLine() ?? string.Empty;

            var sortFunction = exportFunctions.FirstOrDefault(sf => sf.OptionCode == option);
            if (sortFunction == null)
            {
                Console.WriteLine("Please enter a valid sort option");
                continue;
            }

            await sortFunction.Action();
            break;
        }
    }
}