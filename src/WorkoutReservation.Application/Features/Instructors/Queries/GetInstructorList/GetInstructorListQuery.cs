using MediatR;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

public class GetInstructorListQuery : IRequest<List<InstructorListQueryDto>>
{
}
