using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommand : IRequest
{
    public Guid UserGuid { get; set; }
    public UserRole UserRole { get; set; }
}
