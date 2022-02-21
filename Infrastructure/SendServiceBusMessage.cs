using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using CAR_RENTAL_SERVICE.Models;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    public class SendServiceBusMessage
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration;
        public ServiceBusClient _client;
        public ServiceBusSender _clientSender;
        public SendServiceBusMessage(IConfiguration _configuration,
            ILogger<SendServiceBusMessage> logger)
        {
            _logger = logger;
            var _serviceBusConnectionString = _configuration["ServiceBusConnectionString"];
            string _queueName = _configuration["ServiceBusQueueName"];
            _client = new ServiceBusClient(_serviceBusConnectionString);
            _clientSender = _client.CreateSender(_queueName);
        }
        public async Task sendServiceBusMessage(ServiceBusMessageData Message)
        {
            var messagePayload = JsonSerializer.Serialize(Message);
            ServiceBusMessage ServiceBusMessageData = new ServiceBusMessage(messagePayload);
            try
            {
                await _clientSender.SendMessageAsync(ServiceBusMessageData);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

    }
}
