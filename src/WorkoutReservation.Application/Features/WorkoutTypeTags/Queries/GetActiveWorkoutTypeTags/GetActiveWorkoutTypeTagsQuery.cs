using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;

public record GetActiveWorkoutTypeTagsQuery : IRequest<List<ActiveWorkoutTypeTagsDto>>
{
}