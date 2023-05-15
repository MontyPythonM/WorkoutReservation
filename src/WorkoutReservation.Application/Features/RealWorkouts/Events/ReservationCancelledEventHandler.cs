using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Events;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Application.Features.RealWorkouts.Events;

public class ReservationCancelledEventHandler : INotificationHandler<ReservationCancelledEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailBuilder _emailBuilder;
    private readonly IRealWorkoutRepository _realWorkoutRepository;

    public ReservationCancelledEventHandler(IEmailSender emailSender, IEmailBuilder emailBuilder, 
        IRealWorkoutRepository realWorkoutRepository)
    {
        _emailSender = emailSender;
        _emailBuilder = emailBuilder;
        _realWorkoutRepository = realWorkoutRepository;
    }
    
    public async Task Handle(ReservationCancelledEvent @event, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository
            .GetByReservationIdAsync(@event.ReservationId, true, token);

        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), @event.ReservationId.ToString());
        
        var reservation = realWorkout.Reservations
            .Single(reservation => reservation.Id == @event.ReservationId);

        var instructorFullName = string.Join(" ", realWorkout.Instructor.FirstName, realWorkout.Instructor.LastName);
        
        var message = _emailBuilder.CreateSendGridMessage(reservation.User.Email, 
            $"Your reservation with ID: {@event.ReservationId} on {realWorkout.WorkoutType.Name} with {instructorFullName} was cancelled.", 
            $"Reservation cancelled - {realWorkout.WorkoutType.Name}");
        
        await _emailSender.SendEmail(message, token);
    }
}