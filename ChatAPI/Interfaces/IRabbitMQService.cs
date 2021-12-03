using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAPI.Interfaces
{
    public interface IRabbitMQService
    {
        IConnection CreateChannel();
    }
}
