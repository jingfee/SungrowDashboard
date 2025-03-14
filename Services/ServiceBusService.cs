using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using SungrowDashboard.Models;

namespace SungrowDashboard.Services;

public class ServiceBusService : IServiceBusService
{
    private readonly ServiceBusClient _client;

    public ServiceBusService(ServiceBusClient client)
    {
        _client = client;
    }

    public async Task<List<QueueMessage>> ReceiveMessages()
    {
        ServiceBusReceiver receiver = _client.CreateReceiver("battery-queue");
        IReadOnlyList<ServiceBusReceivedMessage>  messages = await receiver.PeekMessagesAsync(100);

        List<QueueMessage> queueMessages = new List<QueueMessage>();
        foreach(ServiceBusReceivedMessage message in messages) {
            var parsedBody = JsonSerializer.Deserialize<QueueBody>(message.Body.ToString());
            QueueMessage queueMessage = new QueueMessage() {
                SequenceNumber = message.SequenceNumber,
                ScheduledDateTime = message.ScheduledEnqueueTime,
                Body = parsedBody!
            };
            queueMessages.Add(queueMessage);
        }

        return queueMessages;
    }

    public async Task RemoveMessage(long sequenceNumber) {
        ServiceBusSender sender = _client.CreateSender("battery-queue");
        await sender.CancelScheduledMessageAsync(sequenceNumber);
    }

    public async Task<long> AddMessage(QueueMessage queueMessage) {
        ServiceBusSender sender = _client.CreateSender("battery-queue");
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(queueMessage.Body, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
        return await sender.ScheduleMessageAsync(serviceBusMessage, queueMessage.ScheduledDateTime);
    }
}
