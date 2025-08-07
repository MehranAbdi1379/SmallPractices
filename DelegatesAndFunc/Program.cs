// See https://aka.ms/new-console-template for more information

delegate int MathOperation(int x, int y);

internal class Program
{
    public static void Main(string[] args)
    {
        DoSomethingAfterMath(Divide,2,4);
        return;
        double Multiply(int x, int y) => x * y;
        double Subtract(int x, int y) => x - y;
        double Add(int x, int y) => x + y;
        double Divide(int x, int y) => (double)x / y;
    }

    private static void DoSomethingAfterMath(Func<int,int,double> operation, int x, int y)
    {
        Console.WriteLine(operation(x, y));
    }
}
