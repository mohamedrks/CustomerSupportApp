using ChatAPI.Interfaces;
using ChatAPI.Models;
using ChatAPI.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IConnection = RabbitMQ.Client.IConnection;

namespace ChatAPI.Services
{
    public class AgentConsumerService : IAgentConsumerService , IDisposable
    {
        private readonly IRabbitMQService _rabbitMqService;
        private readonly IModel _model;
        private readonly IConnection _connection;

        private readonly IAgentQueueFactory _agentQueueFactory;

        //private readonly IHubContext<ChatHub> _hub;


        //const string _agentQueue1 = "AgentQueue1";
        //const string _agentQueue2 = "AgentQueue2";

        public static List<AgentUser> _agentList = new List<AgentUser>();

        public AgentConsumerService(
            IRabbitMQService rabbitMqService,
            IAgentQueueFactory agentQueueFactory
            //IHubContext<ChatHub> hub
            )
        {

            _agentQueueFactory = agentQueueFactory;

            //_hub = hub;
            _rabbitMqService = rabbitMqService;
            _connection = _rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();

            _model.ExchangeDeclare(exchange: "agentQueueExchange", type: ExchangeType.Fanout, durable: true, autoDelete: false);

            // Bind all the queues here with current shift agents.
        }

        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine("Reading Message -->" + text);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);

                // Add Logic to assign client to an agent.

                // We have the connection Id of agent. we have connection Id of client here it self.
                //await _hub.Groups.Add("", "");
                //await _hub.Groups.Add("", "");

                var groupName = "Agent1";

                //var res = _hub.Clients.Group(groupName).StartingChatAssignGroup(groupName);

            };
            _model.BasicConsume(_agentQueue1, false, consumer);
            await Task.CompletedTask;
        }

        public async Task SendMessgaes(AgentQueue agentQueue,Team team ,string connectionId,string message)
        {
            _model.QueueDeclare(agentQueue.Name, durable: true, exclusive: false, autoDelete: true);
            _model.QueueBind(agentQueue.Name, "agentQueueExchange", string.Empty);


            var body = Encoding.UTF8.GetBytes(message);
            _model.BasicPublish(exchange: "agentQueueExchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            Console.WriteLine(" Sending message {0}", message);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }

    }
}
