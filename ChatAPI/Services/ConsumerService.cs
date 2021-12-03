using ChatAPI.Interfaces;
using ChatAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Services
{
    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        private readonly IAgentConsumerService _agentConsumerService;
        private readonly IAgentFactoryService _agentFactoryService;
        private readonly IAgentQueueFactory _agentQueueFactory;

        const string _queueName = "sessionQueue";

        public ConsumerService
        (
            IRabbitMQService rabbitMqService,
            IAgentConsumerService agentConsumerService,
            IAgentFactoryService agentFactoryService,
            IAgentQueueFactory agentQueueFactory
        )
        {
            _agentConsumerService = agentConsumerService;
            _agentFactoryService = agentFactoryService;
            _agentQueueFactory = agentQueueFactory;


            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("sessionQueueExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, "sessionQueueExchange", string.Empty);
        }

        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);

                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);

                var nextAvailableAgentQueue = _agentQueueFactory.GetNextAgentQueue();
                var getCurrentShiftTeam = _agentFactoryService.GetCurrentShiftTeam();

                // Logic to check which agent to assign here. send agent details , agent queue , incoming client connectionId.

                await _agentConsumerService.SendMessgaes(nextAvailableAgentQueue,getCurrentShiftTeam, text, "Connecting Agent");

            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        public async Task SendMessgaes(ClientUser user)
        {

            var body = Encoding.UTF8.GetBytes(user.ConnectionId);
            _model.BasicPublish(exchange: "sessionQueueExchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            Console.WriteLine(" Sending message {0}", user.MsgText);
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
