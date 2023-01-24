using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.UpdateWorkoutTypeTag;

public class UpdateWorkoutTypeTagCommand : IRequest
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; }
}