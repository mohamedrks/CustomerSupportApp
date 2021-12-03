using ChatAPI.Interfaces;
using ChatAPI.Models;
using ChatAPI.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Utilities
{
    [HubName("ChatHub")]
    public class ChatHub : Hub                                              
    {
        private static List<ClientUser> client = new List<ClientUser>();
        private readonly IConsumerService _consumerService;

        public ChatHub(IConsumerService consumerService)
        {
            _consumerService = consumerService;

            Console.WriteLine("Inside Chat Hub");
        }

        public async Task StartMessage(string userType , string user, string connectionId)
        {
            if (userType.Equals("Agent"))
            {
                AgentConsumerService._agentList.Add(new AgentUser { User = user, ConnectionId = connectionId });
            }
            else
            {              
                client.Add(new ClientUser { User = user, ConnectionId = connectionId });

                // temp add client to session queue.
                await _consumerService.SendMessgaes(new ClientUser() { User = user , ConnectionId = connectionId });

            }

        }

        public async Task StartingChatAssignGroup(string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupName", groupName);
        }

        public async Task SendMessage(string user, string message, string connectionId)
        {
            await Clients.Group(connectionId).SendAsync("ReceiveMessage", user, message);
        }


        public async Task AddToGroup(string agentConnectionId, string clientConnectionId , string groupName)
        {
            await Groups.AddToGroupAsync(agentConnectionId, groupName);
            await Groups.AddToGroupAsync(clientConnectionId, groupName);
        }

    }

}
