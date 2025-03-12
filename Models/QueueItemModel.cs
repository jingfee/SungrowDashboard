using System.ComponentModel.DataAnnotations;

public class QueueItemModel : IValidatableObject
{
    [Required()]
    public string? Operation { get; set; }
    [Required()]
    public DateTime? Date { get; set; }
    [Required()]
    public TimeSpan? Time { get; set; }
    public int? Rank { get; set; }
    [RequiredIf("Operation", "0")]
    public int? Power { get; set; }
    [RequiredIf("Operation", "0", "1")]
    public decimal? TargetSoc { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        // Hämta alla properties med RequiredIf-attribut
        var properties = GetType().GetProperties()
        .Where(prop => prop.GetCustomAttributes(typeof(RequiredIfAttribute), true).Any());

        foreach (var property in properties)
        {
            foreach (RequiredIfAttribute attribute in property.GetCustomAttributes(typeof(RequiredIfAttribute), true))
            {
                var result = attribute.GetValidationResult(property.GetValue(this), validationContext);
                if (result != ValidationResult.Success)
                {
                    results.Add(result!);
                }
            }
        }

        return results;
    }
}