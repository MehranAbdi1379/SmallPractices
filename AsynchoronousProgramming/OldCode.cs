namespace AsynchoronousProgramming;

public class OldCode
{
    public async Task Do()
    {
        await DoSomeAsyncStuff2();
        await Task.Delay(1993);
        Console.WriteLine("Third Ended");
        await Task.Delay(3000);

        await AllMethods();
        Console.WriteLine("Process Is Done!!!!");
    }

    private static async Task DoSomeAsyncStuff()
    {
        await Task.Delay(2000);
        Console.WriteLine("First Ended" + DateTime.Now.Microsecond);
    }

    private static async Task DoSomeAsyncStuff2()
    {
        DoSomeAsyncStuff();
        Console.WriteLine("Second Ended" + DateTime.Now.Microsecond);
    }

    private static async Task AllMethods()
    {
        var firstTask = FirstMethod();
        var secondTask = SecondMethod();
        var thirdTask = ThirdMethod();
        Task.WaitAll(firstTask, secondTask, thirdTask);
    }


    private static async Task ThirdMethod()
    {
        await Task.Delay(2000);
        Console.WriteLine("Third Ended");
    }

    private static async Task SecondMethod()
    {
        await Task.Delay(2000);
        Console.WriteLine("Second Ended");
    }

    private static async Task FirstMethod()
    {
        await Task.Delay(2000);
        Console.WriteLine("First Ended");
    }
}