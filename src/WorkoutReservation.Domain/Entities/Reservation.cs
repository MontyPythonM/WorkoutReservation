using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class Reservation : Entity
{
    public int Id { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; }
    public string Note { get; private set; }
    public RealWorkout RealWorkout { get; private set; }
    public int RealWorkoutId { get; private set; }
    public ApplicationUser User { get; private set; }
    public Guid UserId { get; private set; }

    private Reservation()
    {
        // required for EF Core
    }
    
    internal Reservation(RealWorkout realWorkout, ApplicationUser user)
    {
        RealWorkout = realWorkout;
        User = user;
        ReservationStatus = ReservationStatus.Reserved;
        Note = string.Empty;
        Valid();
    }

    internal void Update( string note, ReservationStatus status)
    {
        Note = note;
        ReservationStatus = status;
        Valid();
    }

    internal void SetReservationStatus(ReservationStatus status)
    {
        ReservationStatus = status;
        Valid();
    }
    
    internal void SetNote(string note)
    {
        Note = note;
        Valid();
    }

    protected override void Valid()
    {
        // TODO add validation for enum 
        
        if (RealWorkout is null)
            throw new DomainException(this, nameof(RealWorkout), ExceptionCode.CannotBeNull);
        
        if (User is null)
            throw new DomainException(this, nameof(User), ExceptionCode.CannotBeNull);
        
        if (Note.Length > 3000)
            throw new DomainException(this, nameof(Note), ExceptionCode.ValueToLarge);
    }
}
