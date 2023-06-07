using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;

public record GetReservationDetailsQuery(int ReservationId) : IRequest<ReservationDetailsDto>;
    
public class GetReservationDetailsHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetReservationDetailsHandler(IReservationRepository reservationRepository, 
        ICurrentUserAccessor currentUserAccessor, IDateTimeProvider dateTimeProvider)
    {
        _reservationRepository = reservationRepository;
        _currentUserAccessor = currentUserAccessor;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ReservationDetailsDto> Handle(GetReservationDetailsQuery request, CancellationToken token)
    {
        var reservation = await _reservationRepository.GetDetailsByIdAsync(request.ReservationId, true, token);
        if (reservation is null)
            throw new NotFoundException(nameof(Reservation), request.ReservationId.ToString());
        
        var userPermissions = _currentUserAccessor.GetUserPermissions();

        if (userPermissions.Contains(Permission.GetSomeoneReservationDetails.ToString()) || 
            userPermissions.Contains(Permission.GetOwnReservationDetails.ToString()) && reservation.UserId == _currentUserAccessor.GetUserId())
        {
            return new ReservationDetailsDto
            {
                Id = reservation.Id,
                CreationDate = reservation.CreatedDate,
                LastModificationDate = reservation.LastModifiedDate,
                ReservationStatus = reservation.ReservationStatus,
                IsWorkoutExpired = _dateTimeProvider.CheckIsExpired(reservation.RealWorkout.Date, reservation.RealWorkout.EndTime),
                Note = reservation.Note,
                RealWorkoutId = reservation.RealWorkoutId,
                MaxParticipantNumber = reservation.RealWorkout.MaxParticipantNumber,
                CurrentParticipantNumber = reservation.RealWorkout.CurrentParticipantNumber,
                StartTime = reservation.RealWorkout.StartTime,
                EndTime = reservation.RealWorkout.EndTime,
                Date = reservation.RealWorkout.Date,
                WorkoutTypeId = reservation.RealWorkout.WorkoutType.Id,
                WorkoutTypeName = reservation.RealWorkout.WorkoutType.Name,
                Intensity = reservation.RealWorkout.WorkoutType.Intensity,
                InstructorId = reservation.RealWorkout.Instructor.Id,
                InstructorFullName = string.Join(" ", reservation.RealWorkout.Instructor.FirstName, reservation.RealWorkout.Instructor.LastName),
            };
        }
        
        throw new UserCannotSeeOtherUsersReservationException();
    }
}