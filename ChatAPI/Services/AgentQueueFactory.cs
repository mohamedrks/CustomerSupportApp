using ChatAPI.Interfaces;
using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Services
{
    public class AgentQueueFactory : IAgentQueueFactory
    {
        private static List<AgentQueue> agentQueueList = new List<AgentQueue>();


        public AgentQueueFactory()
        {
            agentQueueList.Add(new AgentQueue() {  Name = "Agent1" , Availablity = true });
            agentQueueList.Add(new AgentQueue() {  Name = "Agent2" , Availablity = true });
            agentQueueList.Add(new AgentQueue() {  Name = "Agent3" , Availablity = true });
            agentQueueList.Add(new AgentQueue() {  Name = "Agent4" , Availablity = true });
            agentQueueList.Add(new AgentQueue() {  Name = "Agent5" , Availablity = true });
            agentQueueList.Add(new AgentQueue() {  Name = "Agent6" , Availablity = true });

        }

        public AgentQueue GetNextAgentQueue()
        {
            var nextAvailableQueue = agentQueueList.Where(a => a.Availablity == true).FirstOrDefault();
            nextAvailableQueue.Availablity = false;

            return nextAvailableQueue;
        }
    }
}
