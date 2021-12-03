using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Interfaces
{
    public interface IAgentQueueFactory
    {
        public AgentQueue GetNextAgentQueue();
    }
}
