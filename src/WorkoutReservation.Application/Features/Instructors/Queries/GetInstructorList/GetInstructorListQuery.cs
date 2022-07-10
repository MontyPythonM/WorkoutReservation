using MediatR;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

public class GetInstructorListQuery : IRequest<List<InstructorListQueryDto>>
{
}
