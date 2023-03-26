namespace WorkoutReservation.Domain.Abstractions;

public abstract class Entity
{
    public string CreatedBy { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string LastModifiedBy  { get; protected set; } = string.Empty;
    public DateTime? LastModifiedDate  { get; protected set; }

    protected Entity()
    {
        // required by EF core
    }
    
    protected abstract void Valid();
}