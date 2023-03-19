using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
}