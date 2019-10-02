using Dry.Cancellations;
using static System.Console;
using System;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new Cancellation(5000))
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
    }
}
