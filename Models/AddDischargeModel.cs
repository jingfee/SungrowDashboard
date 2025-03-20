using System.ComponentModel.DataAnnotations;

public class AddDischargeModel
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
    public bool AddRank {get;set;} = false;
}