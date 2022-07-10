using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;

public class GetWorkoutTypeDetailQuery : IRequest<WorkoutTypeDetailQueryDto>
{
    public int WorkoutTypeId { get; set; }
}
