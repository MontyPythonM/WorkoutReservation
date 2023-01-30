using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommand : IRequest
{
    public Guid UserId { get; set; }
    public Role Role { get; set; }
}
