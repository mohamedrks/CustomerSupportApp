using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AgentConsole
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Enter your Agent name");
            string name = Console.ReadLine();

            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/ChatHub").Build();
            connection.On<string>("SendMessage", update =>
            {
                // this.Dispatcher.Invoke(() => lblContent.Content = update);
                Console.WriteLine("Message ---> " + update);

            });

            await connection.StartAsync();

            await connection.InvokeAsync("StartMessage", "Agent", "Riki", connection.ConnectionId);


            connection.On<string>("ReceiveGroupName", gName => Console.WriteLine("Group Name is " + gName));
        }
    }
}
