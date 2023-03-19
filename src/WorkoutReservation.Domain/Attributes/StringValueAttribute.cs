namespace WorkoutReservation.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class StringValueAttribute : Attribute
{
    public string Value { get; set; }

    public StringValueAttribute(string value)
    {
        Value = value;
    }
}