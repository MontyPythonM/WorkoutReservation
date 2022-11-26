using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _userService;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public CancelReservationCommandHandler(IReservationRepository reservationRepository,
                                           IUserRepository userRepository,
                                           ICurrentUserService userService,
                                           IRealWorkoutRepository realWorkoutRepository,
                                           IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
        _userService = userService;
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CancelReservationCommand request, 
                                   CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationId, cancellationToken);

        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");

        var currentUserGuid = Guid.Parse(_userService.UserId);
       
        var validator = new CancelReservationCommandValidator(reservation, currentUserGuid);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var mappedReservation = _mapper.Map<Reservation>(request);

        mappedReservation.LastModificationDate = DateTime.Now;
        mappedReservation.ReservationStatus = ReservationStatus.Cancelled;
        mappedReservation.CreationDate = reservation.CreationDate;
        mappedReservation.UserId = reservation.UserId;
        mappedReservation.RealWorkoutId = reservation.RealWorkoutId;

        await _reservationRepository
            .UpdateReservationAsync(mappedReservation, cancellationToken);

        await _realWorkoutRepository
            .DecrementCurrentParticipantNumberAsync(reservation.RealWorkout, cancellationToken);

        return Unit.Value;
    }
}
