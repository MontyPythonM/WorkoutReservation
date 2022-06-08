using MediatR;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList
{
    public class GetWorkoutTypesListQuery : IRequest<List<WorkoutTypesListDto>>
    {

    }
}
