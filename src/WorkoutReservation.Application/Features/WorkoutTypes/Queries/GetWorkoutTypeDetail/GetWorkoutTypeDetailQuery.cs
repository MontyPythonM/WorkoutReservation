using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail
{
    public class GetWorkoutTypeDetailQuery : IRequest<WorkoutTypeDetailDto>
    {
        public int WorkoutTypeId { get; set; }
    }
}
