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

    public string OptionCode => "update";
    public string Description => "Updated contact";

    public async Task Action()
    {
        while (true)
        {
            Console.Write("Please enter name: ");
            var name = Console.ReadLine() ?? string.Empty;
            var updateContact = _db.GetQueryable().FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
            if (updateContact == null)
            {
                Console.WriteLine("Contact does not exist");
                continue;
            }

            Console.Write("Please enter phone number: ");
            var phoneNumber = Console.ReadLine() ?? updateContact.Phone;
            if (string.IsNullOrEmpty(phoneNumber)) phoneNumber = updateContact.Phone;
            updateContact.Phone = phoneNumber;

            var favorite = false;
            Console.Write("Is this contact a Favorite?");
            var favoriteString = Console.ReadLine() ?? "no";
            if (favoriteString.ToLower() == "yes" || favoriteString == "")
                favorite = true;
            updateContact.Favorite = favorite;

            await _db.UpdateAsync(updateContact);
            Console.WriteLine("Contact updated");
            break;
        }
    }
}