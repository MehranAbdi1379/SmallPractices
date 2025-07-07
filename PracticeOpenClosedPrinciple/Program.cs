// See https://aka.ms/new-console-template for more information

using PracticeOpenClosedPrinciple;
using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;
using PracticeOpenClosedPrinciple.Services;

var contactRepository = new MongoDbContext<Contact>();

var contactFunctions = new List<IContactFunction>
{
    new DisplayAllContactsFunction(contactRepository),
    new AddContactFunction(contactRepository),
    new UpdateContactFunction(contactRepository),
    new DeleteContactFunction(contactRepository),
    new ExportContactsToFileFunction(contactRepository),
    new SearchContactsByNameFunction(contactRepository)
};

MongoClassMapping.RegisterClassMaps();

Console.WriteLine("Options of the application: ");
foreach (var function in contactFunctions) Console.WriteLine($"{function.OptionCode}. {function.Description}");

while (true)
{
    Console.Write("Please enter option: ");
    var option = Console.ReadLine() ?? "";

    var function = contactFunctions.FirstOrDefault(f => f.OptionCode == option);
    if (function == null) Console.WriteLine("Please enter a valid option");
    else await function.Action();
    Console.WriteLine();
}