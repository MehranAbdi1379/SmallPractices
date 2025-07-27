using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class SortContactsFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public SortContactsFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "sort";
    public string Description => "Sorts contacts";

    public async Task Action()
    {
        var sortFunctions = new List<IContactSortFunction>
        {
            new ContactSortAlphabeticallyAsc(_db),
            new ContactSortAlphabeticallyDesc(_db),
            new ContactSortPhoneNumberLength(_db)
        };

        foreach (var function in sortFunctions) Console.WriteLine($"{function.OptionCode}: {function.Description}");

        while (true)
        {
            Console.Write("Please enter sort option: ");
            var option = Console.ReadLine() ?? string.Empty;

            var sortFunction = sortFunctions.FirstOrDefault(sf => sf.OptionCode == option);
            if (sortFunction == null)
            {
                Console.WriteLine("Please enter a valid sort option");
                continue;
            }

            await sortFunction.Action();
            return;
        }
    }
}