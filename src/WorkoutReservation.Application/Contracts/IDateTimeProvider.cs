namespace WorkoutReservation.Application.Contracts;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public DateOnly CalculateDateInCurrentWeek(DayOfWeek dayOfWeek);
    public DateTime CalculateDateTimeInCurrentWeek(DayOfWeek dayOfWeek, TimeOnly timeOnly);
}