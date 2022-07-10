using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;

public class CreateInstructorCommand : IRequest<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public string Biography { get; set; }
    public string Email { get; set; }
}
