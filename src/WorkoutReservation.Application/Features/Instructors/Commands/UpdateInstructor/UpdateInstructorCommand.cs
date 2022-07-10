using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;

public class UpdateInstructorCommand : IRequest
{
    public int InstructorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public string Biography { get; set; }
    public string Email { get; set; }
}
