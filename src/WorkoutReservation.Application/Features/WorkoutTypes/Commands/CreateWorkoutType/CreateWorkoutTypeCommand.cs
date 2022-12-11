using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;

public class CreateWorkoutTypeCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkoutIntensity Intensity { get; set; }
    public List<int> WorkoutTypeTags { get; set; }
}
