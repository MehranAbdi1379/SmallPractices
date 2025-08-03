namespace CancelationTokens;

public static class OldCode
{
    public static async Task DoSomething()
    {
        try
        {
            await RunCpuHardProblemWithToken();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Calculation was cancelled!");
        }

        return;


        static async Task RunCpuHardProblemWithToken()
        {
            using var ct = new CancellationTokenSource();

            _ = Task.Run(() =>
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Console.WriteLine("\n");
                ct.Cancel();
            });

            await RunProblem(ct.Token);

            static Task RunProblem(CancellationToken ct)
            {
                var numbers = new List<int>();
                var n = 1000_000_00;
                for (var i = 0; i < n; i++)
                {
                    if (i % (n / 100) == 0)
                    {
                        double percentage = (long)i * 100 / n;
                        Console.WriteLine($"Listing is {percentage}% Done!");
                    }

                    ct.ThrowIfCancellationRequested();
                    numbers.Add(i);
                }

                Shuffle(numbers, ct);
                var m = 1000_000;
                var indexes = new List<int>();
                for (var i = 0; i < m; i++)
                {
                    if (i % (m / 10000) == 0)
                    {
                        var percentage = (double)i * 100 / m;
                        Console.WriteLine($"Indexing is {percentage}% Done!");
                    }

                    ct.ThrowIfCancellationRequested();
                    indexes.Add(numbers.IndexOf(i));
                }

                foreach (var number in numbers) Console.WriteLine(number);
                return Task.CompletedTask;
            }

            static void Shuffle<T>(IList<T> list, CancellationToken cancellationToken)
            {
                var rng = new Random(); // You can make this static if calling multiple times
                var n = list.Count;

                for (var i = n - 1; i > 0; i--)
                {
                    if (i % (n / 100) == 0) Console.WriteLine($"Shuffle is {(long)(n - i) * 100 / n}% Done!");
                    cancellationToken.ThrowIfCancellationRequested();
                    var j = rng.Next(i + 1); // 0 ≤ j ≤ i
                    (list[i], list[j]) = (list[j], list[i]); // C# 7.0 tuple swap
                }
            }
        }

        static async Task RunDownload()
        {
            using var ct = new CancellationTokenSource();

            _ = Task.Run(() =>
            {
                Console.WriteLine("Press any key to cancel the download...");
                Console.ReadKey();
                Console.WriteLine("\nCancelling");
                ct.Cancel();
            });

            try
            {
                await DownloadFile(ct.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Download was cancelled!");
            }

            return;

            static async Task DownloadFile(CancellationToken ct)
            {
                Console.WriteLine("Downloading file started...");
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Downloading file {i * 10}%");
                    await Task.Delay(1000, ct);
                }

                Console.WriteLine("Downloading file finished...");
            }
        }
    }
}