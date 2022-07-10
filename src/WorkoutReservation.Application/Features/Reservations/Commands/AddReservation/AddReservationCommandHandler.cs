using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ICurrentUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AddReservationCommandHandler(IReservationRepository reservationRepository, 
                                        IRealWorkoutRepository realWorkoutRepository,
                                        ICurrentUserService userService,
                                        IUserRepository userRepository,
                                        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _userService = userService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddReservationCommand request,    
                                  CancellationToken cancellationToken)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdWithReservationDetailsAsync(request.RealWorkoutId);
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");

        var currentUserGuid = Guid.Parse(_userService.UserId);

        var isUserAlreadyReservedWorkout = await _reservationRepository
            .CheckUserReservationAsync(request.RealWorkoutId, currentUserGuid);

        var validator = new AddReservationCommandValidator(realWorkout, 
                                                           currentUserGuid, 
                                                           isUserAlreadyReservedWorkout);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        await _realWorkoutRepository.IncrementCurrentParticipianNumber(realWorkout);

        var reservation = _mapper.Map<Reservation>(request);

        reservation.RealWorkoutId = realWorkout.Id;
        reservation.UserId = currentUserGuid;
        reservation.CreationDate = DateTime.Now;
        reservation.ReservationStatus = ReservationStatus.Reserved;

        reservation = await _reservationRepository.AddReservationAsync(reservation);

        return reservation.Id;
    }
}
