using MediatR;

namespace WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor
{
    public  class DeleteInstructorCommand : IRequest
    {
        public int InstructorId { get; set; }
    }
}
