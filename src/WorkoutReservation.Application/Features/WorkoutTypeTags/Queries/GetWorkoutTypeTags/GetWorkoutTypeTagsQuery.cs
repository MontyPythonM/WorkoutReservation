using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;

public record GetWorkoutTypeTagsQuery : IRequest<List<WorkoutTypeTagsDto>>
{
}