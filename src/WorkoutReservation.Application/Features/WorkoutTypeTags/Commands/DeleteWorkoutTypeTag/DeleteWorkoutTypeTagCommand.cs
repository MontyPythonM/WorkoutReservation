using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeleteWorkoutTypeTag;

public class DeleteWorkoutTypeTagCommand : IRequest
{
    public int Id { get; set; }
}