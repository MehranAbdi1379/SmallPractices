using PracticeOpenClosedPrinciple.Infrastructure;
using PracticeOpenClosedPrinciple.Model;

namespace PracticeOpenClosedPrinciple.Services;

public class UpdateContactFunction : IContactFunction
{
    private readonly MongoDbContext<Contact> _db;

    public UpdateContactFunction(MongoDbContext<Contact> db)
    {
        _db = db;
    }

    public string OptionCode => "3";
    public string Description => "Updated contact";

    public async Task Action()
    {
        Console.Write("Please enter name: ");
        var name = Console.ReadLine() ?? string.Empty;
        var updateContact = _db.GetQueryable().FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        while (updateContact == null)
        {
            Console.WriteLine("Contact does not exist");
            return;
        }

        Console.Write("Please enter phone number: ");
        var phoneNumber = Console.ReadLine() ?? updateContact.Phone;
        updateContact.Phone = phoneNumber;
        await _db.UpdateAsync(updateContact);
        Console.WriteLine("Contact updated");
    }
}