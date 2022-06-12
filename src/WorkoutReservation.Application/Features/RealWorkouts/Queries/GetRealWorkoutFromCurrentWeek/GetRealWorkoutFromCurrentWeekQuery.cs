using MediatR;
using WorkoutReservation.Application.Common.Models;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek
{
    public class GetRealWorkoutFromCurrentWeekQuery : IRequest<List<RealWorkoutListDto>>
    {
    }
}
