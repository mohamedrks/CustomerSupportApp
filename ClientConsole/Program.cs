using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        private static async Task Main(string[] args)
        {

            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/ChatHub").Build();
            connection.On<string>("SendMessage", update =>
            {
                
                Console.WriteLine("Message ---> " + update);
                
            });

            await connection.StartAsync();
            await connection.InvokeAsync("StartMessage", "Client", "Riki", connection.ConnectionId);


            connection.On<string>("ReceiveGroupName", gName => Console.WriteLine("Group Name is " + gName ));
            Console.Read();
        }
    }
}

