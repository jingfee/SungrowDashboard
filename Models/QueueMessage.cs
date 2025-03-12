using System.Text.Json.Serialization;

namespace SungrowDashboard.Models;

public class QueueMessage
{
    public long? SequenceNumber { get; set; }
    public DateTimeOffset? ScheduledDateTime { get; set; }
    public QueueBody? Body {get;set;}

}

public class QueueBody {
    [JsonPropertyName("operation")]
    public Operation? Operation { get; set; }
    [JsonPropertyName("power")]
    public int? Power { get; set; }
    [JsonPropertyName("targetSoc")]
    public decimal? TargetSoc { get; set; }
    [JsonPropertyName("rank")]
    public int? Rank { get; set; }
}

public enum Operation {
    StartCharge,
    StopCharge,
    StartDischarge,
    StopDischarge
}