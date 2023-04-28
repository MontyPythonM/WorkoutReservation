using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkouts;

public record GetRealWorkoutsQuery : IRequest<List<RealWorkoutDto>>;

internal sealed class GetRealWorkoutsQueryHandler : IRequestHandler<GetRealWorkoutsQuery, 
    List<RealWorkoutDto>>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetRealWorkoutsQueryHandler(IRealWorkoutRepository realWorkoutRepository, 
        ICurrentUserAccessor currentUserAccessor, IDateTimeProvider dateTimeProvider)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _currentUserAccessor = currentUserAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<RealWorkoutDto>> Handle(GetRealWorkoutsQuery request, 
        CancellationToken token)
    {
        var firstDayOfCurrentWeek = _dateTimeProvider.GetFirstDayOfCurrentWeek();
        var lastDayOfUpcomingWeek = _dateTimeProvider.GetLastDayOfUpcomingWeek();
        
        var realWorkouts = await _realWorkoutRepository
            .GetAllFromDateRangeAsync(firstDayOfCurrentWeek, lastDayOfUpcomingWeek, true, token,
                incl => incl.WorkoutType, incl => incl.Instructor, incl => incl.Reservations);

        return realWorkouts.Select(rw => new RealWorkoutDto
            {
                Id = rw.Id,
                CurrentParticipantNumber = rw.CurrentParticipantNumber,
                MaxParticipantNumber = rw.MaxParticipantNumber,
                StartDate = rw.Date.ToDateTime(rw.StartTime),
                EndDate = rw.Date.ToDateTime(rw.EndTime),
                IsExpired = _dateTimeProvider.CheckIsExpired(rw.Date, rw.EndTime),
                IsAlreadyReserved = IsAlreadyReserved(rw),
                ReservationId = GetUserReservationId(rw),
                WorkoutTypeId = rw.WorkoutType.Id,
                WorkoutTypeName = rw.WorkoutType.Name,
                WorkoutIntensity = rw.WorkoutType.Intensity,
                InstructorId = rw.Instructor.Id,
                InstructorFullName = string.Join(" ", rw.Instructor.FirstName, rw.Instructor.LastName),
                InstructorShortName = $"{rw.Instructor.FirstName[0]}. {rw.Instructor.LastName}"
            })
            .ToList();
    }

    private bool IsAlreadyReserved(RealWorkout workout)
    {
        if (_currentUserAccessor.IsUserContextExist())
        {
            return workout.Reservations
                .FirstOrDefault(r => r.UserId == _currentUserAccessor.GetUserId() && 
                                     r.ReservationStatus != ReservationStatus.Cancelled) is not null;
        }
        
        return false;
    }

    private int? GetUserReservationId(RealWorkout workout)
    {
        return _currentUserAccessor.IsUserContextExist() ? 
            workout.Reservations.FirstOrDefault(r => r.UserId == _currentUserAccessor.GetUserId() && 
                                                     r.ReservationStatus != ReservationStatus.Cancelled)?.Id : null;
    }
}
