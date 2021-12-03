using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Models
{
    public class Team
    {
        public string Name { get; set; }

        public string ShiftTime { get; set; }

        public double Capacity { get; set; }

        public List<Agent> Members { get; set; }
    }
}
