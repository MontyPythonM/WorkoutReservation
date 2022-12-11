using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public class CreateWorkoutTypeTagCommand : IRequest<int>
{
    public string Tag { get; set; }
}