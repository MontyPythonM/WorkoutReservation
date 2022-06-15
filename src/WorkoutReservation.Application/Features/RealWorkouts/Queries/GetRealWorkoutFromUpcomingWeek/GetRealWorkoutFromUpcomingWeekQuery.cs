using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek
{
    public class GetRealWorkoutFromUpcomingWeekQuery : IRequest<List<RealWorkoutFromUpcomingWeekDto>>
    {
    }
}
