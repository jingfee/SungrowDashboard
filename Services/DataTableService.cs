using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using SungrowDashboard.Models;

namespace SungrowDashboard.Services;

public class DataTableService : IDataTableService
{
    private readonly TableClient _client;

    public DataTableService(TableClient client)
    {
        _client = client;
    }

    public async Task<List<BatteryData>> GetAllDataValues()
    {
        var entities = new List<BatteryData>();

        await foreach (var entity in _client.QueryAsync<TableEntity>())
        {
            var batteryData = new BatteryData
            {
                PartitionKey = entity.PartitionKey,
                Timestamp = entity.Timestamp,
                Value = entity.GetString("value") ?? string.Empty
            };
            entities.Add(batteryData);
        }

        return entities;
    }
}