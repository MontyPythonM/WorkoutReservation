using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList
{
    public class GetUserReservationsListQuery : IRequest<List<UserReservationsListDto>>
    {        
    }
}
