using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Models
{
    public class AgentQueue
    {
        public string Name { get; set; }

        public bool Availablity { get; set; }

        public Agent Agent { get; set; }
    }
}
