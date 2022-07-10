using MediatR;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

public class GetUsersListQuery : IRequest<List<UsersListDto>>
{
}
