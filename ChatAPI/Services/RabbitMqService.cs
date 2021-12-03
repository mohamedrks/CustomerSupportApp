using ChatAPI.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAPI.Services
{
    public class RabbitMqService : IRabbitMQService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }

    }
}
