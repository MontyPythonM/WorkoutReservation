using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;

public record GetReservationDetailsQuery(int ReservationId) : IRequest<ReservationDetailsDto>;
    
public class GetReservationDetailsHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    
    public GetReservationDetailsHandler(IReservationRepository reservationRepository, IMapper mapper, 
        ICurrentUserAccessor currentUserAccessor)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _currentUserAccessor = currentUserAccessor;
    }
    
    public async Task<ReservationDetailsDto> Handle(GetReservationDetailsQuery request, CancellationToken token)
    {
        var reservation = await _reservationRepository.GetDetailsByIdAsync(request.ReservationId, true, token);
        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");
        
        var userPermissions = _currentUserAccessor.GetUserPermissions();

        if (userPermissions.Contains(Permission.GetSomeoneReservationDetails.ToString()) || 
            userPermissions.Contains(Permission.GetOwnReservationDetails.ToString()) && reservation.UserId == _currentUserAccessor.GetUserId())
        {
            return _mapper.Map<ReservationDetailsDto>(reservation);
        }
        
        throw new UnauthorizedException();
    }
}