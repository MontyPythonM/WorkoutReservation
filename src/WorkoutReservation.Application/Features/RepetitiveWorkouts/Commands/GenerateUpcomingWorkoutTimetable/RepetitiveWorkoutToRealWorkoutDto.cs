namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

internal class RepetitiveWorkoutToRealWorkoutDto
{
    public int MaxParticipianNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int WorkoutTypeId { get; set; }
    public int InstructorId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public DateOnly Date { get; set; }
}
