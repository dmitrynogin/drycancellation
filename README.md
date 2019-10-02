# drycancellation
Operation cancellation library

[GitHub](https://github.com/dmitrynogin/drycancellation) and [NuGet](https://www.nuget.org/packages/Dry.Cancellations/)

How many times you have been writing something like this passing those tedious logger/token parameters?

    interface IMyService
    {
        void Method1(…, ILogger logger, CancallationToken token);
        void Method2(…, ILogger logger, CancallationToken token);
        …
    }

Enough is enough. Please see [here](https://github.com/dmitrynogin/drylogs) about ambient logging. Below is about ambient cancellation.

What we about to do is to use a special `Cancellation` helper class like this:

        static void Main(string[] args)
        {
            using (new Cancellation())
            {
                Task.Run(PingAsync);
                ReadLine();
                Cancellation.Request();
                ReadLine();
            }
        }

        static async Task PingAsync()
        {
            try
            {
                while (!Cancellation.Requested)
                {
                    await Task.Delay(100, Cancellation.Token);
                    WriteLine("Ping");
                }

                Cancellation.ThrowIfRequested();
            }
            catch(OperationCanceledException)
            {
                WriteLine("Ping cancelled");
            }
        }

