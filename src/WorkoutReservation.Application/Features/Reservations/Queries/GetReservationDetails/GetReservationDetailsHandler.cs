using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;

public record GetReservationDetailsQuery : IRequest<ReservationDetailsDto>
{
    public int ReservationId { get; set; }
    public Guid UserId { get; set; }
};

public class GetReservationDetailsHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    
    public GetReservationDetailsHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }
    
    public async Task<ReservationDetailsDto> Handle(GetReservationDetailsQuery request, CancellationToken token)
    {
        var reservation = await _reservationRepository
            .GetDetailsByIdAsync(request.ReservationId, request.UserId, true, token);
        
        if (reservation is null)
            throw new NotFoundException($"User don't have reservation with Id: {request.ReservationId}.");

        return _mapper.Map<ReservationDetailsDto>(reservation);
    }
}