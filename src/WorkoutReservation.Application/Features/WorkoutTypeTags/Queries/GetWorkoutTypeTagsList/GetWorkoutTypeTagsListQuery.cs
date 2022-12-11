using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTagsList;

public record GetWorkoutTypeTagsListQuery : IRequest<List<WorkoutTypeTagsListDto>>
{
    public bool OnlyActive { get; set; }
}