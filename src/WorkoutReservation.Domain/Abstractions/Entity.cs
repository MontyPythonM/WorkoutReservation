using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public string CreatedBy { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string LastModifiedBy  { get; protected set; } = string.Empty;
    public DateTime? LastModifiedDate  { get; protected set; }

    protected Entity()
    {
        // required by EF core
    }
    
    protected abstract void Valid();

    protected void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
}