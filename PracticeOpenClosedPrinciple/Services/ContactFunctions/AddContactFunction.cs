using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services.ContactFunctions;

public class AddContactFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public AddContactFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "add";
    public string Description => "Add contact";

    public async Task Action()
    {
        while (true)
        {
            Console.Write("Please enter name: ");
            var name = Console.ReadLine();
            if (_db.GetQueryable().Any(c => c.Name == name))
            {
                Console.WriteLine("Name already exists");
                continue;
            }

            Console.Write("Please enter phone number: ");
            var phone = Console.ReadLine();
            if (_db.GetQueryable().Any(c => c.Phone == phone))
            {
                Console.WriteLine("Phone number already exists");
                continue;
            }

            var favoriteBool = false;
            Console.Write("Is this contact favorite? (yes/no)");
            var favorite = Console.ReadLine() ?? "no";
            if (favorite.ToLower() == "yes")
                favoriteBool = true;

            var newContact = new Contact { Name = name, Phone = phone, Favorite = favoriteBool };
            //contacts.Add(newContact);
            await _db.AddAsync(newContact);
            Console.WriteLine("Contact added");
            break;
        }
    }
}