using ChatAPI.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Services
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly IConsumerService _consumerService;
        private readonly IAgentConsumerService _agentConsumerService;

        public ConsumerHostedService(
            IConsumerService consumerService,
            IAgentConsumerService agentConsumerService
        )
        {
            _consumerService = consumerService;
            _agentConsumerService = agentConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ReadMessgaes();
            await _agentConsumerService.ReadMessgaes();
        }

    }
}
