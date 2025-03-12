using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _otherProperty;
    private readonly HashSet<object> _expectedValues;

    public RequiredIfAttribute(string otherProperty, params object[] expectedValues)
    {
        _otherProperty = otherProperty;
        _expectedValues = new HashSet<object>(expectedValues);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);
        if (otherPropertyInfo == null)
        {
            return new ValidationResult($"Unknown property: {_otherProperty}");
        }

        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (otherPropertyValue != null && _expectedValues.Contains(otherPropertyValue) && value == null)
        {
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required when {_otherProperty} is one of {string.Join(", ", _expectedValues)}.");
        }

        return ValidationResult.Success;
    }
}
