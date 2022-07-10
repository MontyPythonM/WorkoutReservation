using MediatR;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;

public class DeleteRepetitiveWorkoutCommand : IRequest
{
    public int RepetitiveWorkoutId { get; set; }
}
