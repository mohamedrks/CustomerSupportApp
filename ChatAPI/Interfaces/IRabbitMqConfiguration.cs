using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAPI.Interfaces
{
    public interface IRabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
