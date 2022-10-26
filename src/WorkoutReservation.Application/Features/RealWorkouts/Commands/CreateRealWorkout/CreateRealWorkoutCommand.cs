using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;

public class CreateRealWorkoutCommand : IRequest<int>
{
    public int MaxParticipantNumber { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int WorkoutTypeId { get; set; }
    public int InstructorId { get; set; }
}
