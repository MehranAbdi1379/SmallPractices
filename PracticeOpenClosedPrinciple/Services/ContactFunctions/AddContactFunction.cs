using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class AddContactFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public AddContactFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "2";
    public string Description => "Add contact";

    public async Task Action()
    {
        Console.Write("Please enter name: ");
        var name = Console.ReadLine();
        if (_db.GetQueryable().Any(c => c.Name == name))
        {
            Console.WriteLine("Name already exists");
            return;
        }

        Console.Write("Please enter phone number: ");
        var phone = Console.ReadLine();
        var newContact = new Contact { Name = name, Phone = phone };
        //contacts.Add(newContact);
        await _db.AddAsync(newContact);
        Console.WriteLine("Contact added");
    }
}