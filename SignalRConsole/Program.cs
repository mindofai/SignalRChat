using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRConsole
{
    class Program
    {
        private static HubConnection hubConnection;

        static void Main(string[] args)
        {
            hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44398/chathub").Build();

            hubConnection.StartAsync().Wait();

            hubConnection.On<string, string>("ReceiveMessage", (user, receivedMessage) =>
            {
                Console.WriteLine($"{user} said {receivedMessage}");
            });


            Console.Write("Type any message: ");
            var keepGoing = true;
            do
            {
                var text = Console.ReadLine();
                if (text == "exit")
                {
                    keepGoing = false;
                }
                else
                {
                    hubConnection.SendAsync("SendMessage", "SampleUser", text).Wait();
                }
            }
            while (keepGoing);

            Console.ReadKey();
        }
    }
}
