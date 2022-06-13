using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout
{
    public class DeleteRealWorkoutCommand : IRequest
    {
        public int RealWorkoutId { get; set; }
    }
}
