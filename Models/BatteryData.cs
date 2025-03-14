using Azure;
using Azure.Data.Tables;

namespace SungrowDashboard.Models;

public class BatteryData
{
    public required string PartitionKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public string Value { get; set; } = string.Empty;

}