namespace PracticeOpenClosedPrinciple.Services;

public class SortContactsFunction : IContactFunction
{
    public string OptionCode => "7";
    public string Description => "Sorts contacts";

    public async Task Action()
    {
        var sortFunctions = new List<IContactSortFunction>();
        if (sortFunctions == null) throw new ArgumentNullException(nameof(sortFunctions));

        foreach (var function in sortFunctions) Console.WriteLine($"{function.OptionCode}: {function.Description}");

        Console.Write("Please enter sort option: ");
        var option = Console.ReadLine() ?? string.Empty;

        var sortFunction = sortFunctions.FirstOrDefault(sf => sf.OptionCode == option);
        if (sortFunction == null)
        {
            Console.WriteLine("Please enter a valid sort option");
            return;
        }

        await sortFunction.Action();
    }
}