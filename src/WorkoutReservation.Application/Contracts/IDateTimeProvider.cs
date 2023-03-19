namespace WorkoutReservation.Application.Contracts;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}