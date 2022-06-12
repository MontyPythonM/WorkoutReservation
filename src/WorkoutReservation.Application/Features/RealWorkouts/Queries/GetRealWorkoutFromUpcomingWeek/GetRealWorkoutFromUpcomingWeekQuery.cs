using MediatR;
using WorkoutReservation.Application.Common.Models;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek
{
    public class GetRealWorkoutFromUpcomingWeekQuery : IRequest<List<RealWorkoutListDto>>
    {
    }
}
