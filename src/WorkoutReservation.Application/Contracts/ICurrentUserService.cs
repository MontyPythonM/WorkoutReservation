namespace WorkoutReservation.Application.Contracts;

public interface ICurrentUserService
{
    string UserId { get; }
    string UserRole { get; }
}
