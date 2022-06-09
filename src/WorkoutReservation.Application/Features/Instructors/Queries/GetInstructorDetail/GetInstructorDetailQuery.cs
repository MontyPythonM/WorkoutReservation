using MediatR;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail
{
    public class GetInstructorDetailQuery : IRequest<InstructorDetailQueryDto>
    {
        public int InstructorId { get; set; }
    }
}
