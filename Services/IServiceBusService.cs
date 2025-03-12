using SungrowDashboard.Models;

namespace SungrowDashboard.Services;

public interface IServiceBusService
{
    public Task<List<QueueMessage>> ReceiveMessages();
    public Task RemoveMessage(long sequenceNumber);
    public Task<long> AddMessage(QueueMessage queueMessage);
}