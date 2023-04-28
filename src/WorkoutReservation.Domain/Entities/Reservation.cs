using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

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
    
    private const int NoteLengthLimit = 3000;

    protected override void Valid()
    {
        if (!Enum.IsDefined(ReservationStatus))
            throw new ReservationStatusOutOfRangeException();

        if (RealWorkout is null)
            throw new RealWorkoutCannotBeNullException();

        if (User is null)
            throw new ApplicationUserCannotBeNullException();

        if (Note.Length > NoteLengthLimit)
            throw new NoteLengthLimitExceedException(NoteLengthLimit);
    }
}
