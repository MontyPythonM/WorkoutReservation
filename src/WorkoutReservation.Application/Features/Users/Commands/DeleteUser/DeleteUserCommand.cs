using MediatR;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserGuid { get; set; }
    }
}
