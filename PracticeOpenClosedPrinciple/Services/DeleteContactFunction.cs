using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class DeleteContactFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public DeleteContactFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "4";
    public string Description => "Delete contact";

    public async Task Action()
    {
        Console.Write("Please enter name: ");
        var name = Console.ReadLine();
        //var deleteContact = contacts.FirstOrDefault(c => c.Name == name);
        var deleteContact = (await _db.GetAllAsync()).FirstOrDefault(c => c.Name == name);
        if (deleteContact == null)
        {
            Console.WriteLine("Contact does not exist");
            return;
        }

        //contacts.Remove(deleteContact);
        await _db.DeleteAsync(deleteContact);

        Console.WriteLine("Contact deleted");
    }
}