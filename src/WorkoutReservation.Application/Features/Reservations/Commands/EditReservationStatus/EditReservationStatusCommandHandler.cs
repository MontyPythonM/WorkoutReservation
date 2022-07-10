using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

public class EditReservationStatusCommandHandler : IRequestHandler<EditReservationStatusCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public EditReservationStatusCommandHandler(IReservationRepository reservationRepository,
                                               IRealWorkoutRepository realWorkoutRepository,
                                               IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(EditReservationStatusCommand request, 
                                   CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository
            .GetReservationByIdAsync(request.ReservationId);

        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");

        var validator = new EditReservationStatusCommandValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var mappedReservation = _mapper.Map<Reservation>(request);

        mappedReservation.LastModificationDate = DateTime.Now;
        mappedReservation.ReservationStatus = request.ReservationStatus;
        mappedReservation.CreationDate = reservation.CreationDate;
        mappedReservation.UserId = reservation.UserId;
        mappedReservation.RealWorkoutId = reservation.RealWorkoutId;

        await _reservationRepository
            .UpdateReservationAsync(mappedReservation);

        if (reservation.ReservationStatus != ReservationStatus.Cancelled &&
            mappedReservation.ReservationStatus == ReservationStatus.Cancelled)
        {
            await _realWorkoutRepository
                .DecrementCurrentParticipianNumber(reservation.RealWorkout);
        }

        return Unit.Value;
    }
}
