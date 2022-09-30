using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;

public class UpdateRealWorkoutCommand : IRequest
{
    public int RealWorkoutId { get; set; }
    public int MaxParticipiantNumber { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int InstructorId { get; set; }
}
