using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeactivateWorkoutTypeTag;

public class DeactivateWorkoutTypeTagCommand : IRequest
{
    public int Id { get; set; }
}