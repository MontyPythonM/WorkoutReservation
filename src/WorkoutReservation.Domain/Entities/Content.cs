using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class Content : Entity
{
    public Guid Id { get; private set; }
    public ContentType Type { get; private set; }
    public string Value { get; private set; }

    public Content(ContentType type, string value)
    {
        Id = Guid.NewGuid();
        Type = type;
        Value = value;
        Valid();
    }

    public void Update(string value)
    {
        Value = value;
        Valid();
    }

    private const int ValueLengthLimit = 20000;
    
    protected override void Valid()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ContentValueCannotBeNullException();

        if (Value.Length > ValueLengthLimit)
            throw new ContentValueLengthExceedException(ValueLengthLimit);

        if (!Enum.IsDefined(Type))
            throw new ContentTypeOutOfRangeException();
    }
}