using MediatR;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail
{
    public class GetRealWorkoutDetailQuery : IRequest<RealWorkoutDetailDto>
    {
        public int RealWorkoutId { get; set; }
    }
}
