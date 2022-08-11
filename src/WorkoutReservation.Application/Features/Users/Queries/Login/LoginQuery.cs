using MediatR;

namespace WorkoutReservation.Application.Features.Users.Queries.Login;

public class LoginQuery : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
