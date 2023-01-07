using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _userService;

    public AddReservationCommandHandler(IReservationRepository reservationRepository, 
        IRealWorkoutRepository realWorkoutRepository,
        ICurrentUserService userService, 
        IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _userService = userService;
        _userRepository = userRepository;
    }

    public async Task<int> Handle(AddReservationCommand request, CancellationToken token)
    {
        var currentUserGuid = _userService.UserId.ToGuid();
        var user = await _userRepository.GetByGuidAsync(currentUserGuid, token);

        var realWorkout = await _realWorkoutRepository
            .GetByIdAsync(request.RealWorkoutId, false, token);
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");
        
        var isUserAlreadyReservedWorkout = await _reservationRepository
            .CheckIsUserReservedAsync(realWorkout, user, token);
        var validator = new AddReservationCommandValidator(realWorkout, currentUserGuid, isUserAlreadyReservedWorkout);
        await validator.ValidateAndThrowAsync(request, token);

        realWorkout.IncrementCurrentParticipantNumber();
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        
        var reservation = new Reservation(realWorkout, user);
        reservation = await _reservationRepository.AddReservationAsync(reservation, token);
        return reservation.Id;
    }
}
