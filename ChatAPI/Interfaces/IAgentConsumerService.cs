using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Interfaces
{
    public interface IAgentConsumerService
    {
        Task ReadMessgaes();
        Task SendMessgaes(AgentQueue agentQueue, Team team, string connectionId, string message);
    }
}
