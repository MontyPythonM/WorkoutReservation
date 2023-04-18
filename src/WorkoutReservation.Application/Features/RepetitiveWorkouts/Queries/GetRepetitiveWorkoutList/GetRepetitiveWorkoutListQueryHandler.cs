using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

public record GetRepetitiveWorkoutListQuery : IRequest<List<RepetitiveWorkoutListDto>>;

internal sealed class GetRepetitiveWorkoutListQueryHandler : IRequestHandler<GetRepetitiveWorkoutListQuery, 
    List<RepetitiveWorkoutListDto>>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GetRepetitiveWorkoutListQueryHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository, 
        IRealWorkoutRepository realWorkoutRepository, IDateTimeProvider dateTimeProvider)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<RepetitiveWorkoutListDto>> Handle(GetRepetitiveWorkoutListQuery request, 
        CancellationToken token)
    {
        var repetitiveWorkouts = await _repetitiveWorkoutRepository
            .GetAllAsync(true, token, incl => incl.Instructor, incl => incl.WorkoutType);
        
        var upcomingWeekRealWorkouts = await _realWorkoutRepository
            .GetAllFromDateRangeAsync(_dateTimeProvider.GetFirstDayOfUpcomingWeek(), 
                _dateTimeProvider.GetLastDayOfUpcomingWeek(), true, token);
        
        return repetitiveWorkouts.Select(rw => new RepetitiveWorkoutListDto()
            {
                Id = rw.Id,
                MaxParticipantNumber = rw.MaxParticipantNumber,
                StartDate = _dateTimeProvider.CalculateDateTimeInCurrentWeek(rw.DayOfWeek, rw.StartTime),
                EndDate = _dateTimeProvider.CalculateDateTimeInCurrentWeek(rw.DayOfWeek, rw.EndTime),
                DayOfWeek = rw.DayOfWeek,
                CreatedBy = rw.CreatedBy,
                CreatedDate = rw.CreatedDate,
                LastModifiedBy = rw.LastModifiedBy,
                LastModifiedDate = rw.LastModifiedDate,
                WorkoutTypeId = rw.WorkoutType.Id,
                WorkoutTypeName = rw.WorkoutType.Name,
                WorkoutTypeIntensity = rw.WorkoutType.Intensity,
                InstructorId = rw.Instructor.Id,
                InstructorShortName = string.Join(". ", rw.Instructor.FirstName.First(), rw.Instructor.LastName),
                InstructorEmail = rw.Instructor.Email,
                IsRealWorkoutConflict = CheckIsRealWorkoutConflict(rw, upcomingWeekRealWorkouts),
            })
            .ToList();
    }
    
    private bool CheckIsRealWorkoutConflict(RepetitiveWorkout repetitiveWorkout, IEnumerable<RealWorkout> weeklyRealWorkouts)
    {
        foreach (var realWorkout in weeklyRealWorkouts.Where(r => r.Date.DayOfWeek == repetitiveWorkout.DayOfWeek))
        {
            var isCollision =
                (repetitiveWorkout.StartTime >= realWorkout.StartTime && repetitiveWorkout.StartTime < realWorkout.EndTime) ||
                (repetitiveWorkout.EndTime > realWorkout.StartTime && repetitiveWorkout.EndTime <= realWorkout.EndTime) ||
                (repetitiveWorkout.StartTime < realWorkout.StartTime && repetitiveWorkout.EndTime > realWorkout.EndTime);

            if (isCollision)
            {
                return true;
            }
        }
        
        return false;
    }
}
