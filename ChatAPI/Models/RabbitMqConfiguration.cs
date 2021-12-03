using ChatAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQService
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
