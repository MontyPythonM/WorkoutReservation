using MediatR;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;

public class UpdateRepetitiveWorkoutCommand : IRequest
{
    public int RepetitiveWorkoutId { get; set; }
    public int MaxParticipianNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int InstructorId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}
