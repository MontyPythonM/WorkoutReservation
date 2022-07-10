using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;

public class DeleteWorkoutTypeCommand : IRequest
{
    public int WorkoutTypeId { get; set; }
}
