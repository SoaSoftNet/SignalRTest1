using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingalRClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Test();
            Console.ReadKey();

            
        }

        async void Test()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44354/NotificationHub")
                .Build();

            await connection.StartAsync();

            Console.WriteLine("Starting connection. Press Ctrl-C to close.");
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            connection.Closed += e =>
            {
                Console.WriteLine("Connection closed with error: {0}", e);

                cts.Cancel();
                return Task.CompletedTask;
            };


            connection.On<string>("Notify", (a) =>
            {
                Console.WriteLine($"Notify: {a}");
            });
        }
    }
}
