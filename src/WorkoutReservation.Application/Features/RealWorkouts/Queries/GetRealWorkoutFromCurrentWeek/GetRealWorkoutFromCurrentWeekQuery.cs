using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek
{
    public class GetRealWorkoutFromCurrentWeekQuery : IRequest<List<RealWorkoutFromCurrentWeekDto>>
    {
    }
}
