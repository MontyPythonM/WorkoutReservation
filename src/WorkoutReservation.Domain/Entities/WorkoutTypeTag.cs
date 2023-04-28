using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class WorkoutTypeTag : Entity
{
    public int Id { get; private set; }
    public string Tag { get; private set; }
    public bool IsActive { get; private set; } = true;
    public ICollection<WorkoutType> WorkoutTypes { get; private set; } = new List<WorkoutType>();

    private WorkoutTypeTag()
    {
        // required for EF Core
    }

    public WorkoutTypeTag(string tag)
    {
        Tag = tag;
        IsActive = true;
        Valid();
    }

    public void Update(string tag, bool isActive)
    {
        Tag = tag;
        IsActive = isActive;
        Valid();
    }

    private const int TagLengthLimit = 30;

    protected override void Valid()
    {
        if (string.IsNullOrWhiteSpace(Tag))
            throw new WorkoutTypeTagCannotBeNullOfWhitespace();

        if (Tag.Length > TagLengthLimit)
            throw new WorkoutTypeTagLengthExceed(TagLengthLimit);
    }
}
