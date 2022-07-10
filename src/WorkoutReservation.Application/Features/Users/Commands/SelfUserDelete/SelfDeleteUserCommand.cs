using MediatR;

namespace WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;

public class SelfDeleteUserCommand : IRequest
{
    public string Password { get; set; }
}
