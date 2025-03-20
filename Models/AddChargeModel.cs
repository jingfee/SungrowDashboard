using System.ComponentModel.DataAnnotations;

public class AddChargeModel
{
    [Required()]
    public DateTime? StartDate { get; set; }
    [Required()]
    public TimeSpan? StartTime { get; set; }
    [Required()]
    public DateTime? StopDate { get; set; }
    [Required()]
    public TimeSpan? StopTime { get; set; }
    [Required()]
    public int? Power { get; set; }
    [Required()]
    public decimal? TargetSoc { get; set; }
}