using MediatR;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

public class GetRepetitiveWorkoutListQuery : IRequest<List<RepetitiveWorkoutListDto>>
{
}
