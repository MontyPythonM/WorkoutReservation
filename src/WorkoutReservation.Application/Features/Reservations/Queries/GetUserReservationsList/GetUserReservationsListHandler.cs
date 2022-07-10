using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public class GetUserReservationsListHandler : IRequestHandler<GetUserReservationsListQuery, 
                                                              List<UserReservationsListDto>>
{
    private IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;

    public GetUserReservationsListHandler(IReservationRepository reservationRepository, 
                                          IMapper mapper, 
                                          ICurrentUserService userService)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<List<UserReservationsListDto>> Handle(GetUserReservationsListQuery request, 
                                                            CancellationToken cancellationToken)
    {
        var userGuid = Guid.Parse(_userService.UserId);

        var reservations = await _reservationRepository.GetUserReservationsByGuidAsync(userGuid);

        if (!reservations.Any())
            throw new NotFoundException("Reservations not found.");

        return _mapper.Map<List<UserReservationsListDto>>(reservations);
    }
}
