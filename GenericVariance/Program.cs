namespace GenericVariance;

internal class Program
{
    private static void Main(string[] args)
    {
        var animal = new Animal();
        var dog = (Dog)animal;
        var x = 0;
    }
}

public class Animal
{
}

public class Dog : Animal
{
}

public class ObjectComparer : IComparer<object>
{
    public int Compare(object x, object y)
    {
        return 0;
    }
}

public class StringComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        return 0;
    }
}