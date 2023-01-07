using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;

public class GetRealWorkoutFromCurrentWeekQueryHandler : IRequestHandler<GetRealWorkoutFromCurrentWeekQuery, 
    List<RealWorkoutFromCurrentWeekDto>>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRealWorkoutFromCurrentWeekQueryHandler(IRealWorkoutRepository realWorkoutRepository, IMapper mapper)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<List<RealWorkoutFromCurrentWeekDto>> Handle(GetRealWorkoutFromCurrentWeekQuery request, 
        CancellationToken token)
    {
        var firstDayOfCurrentWeek = DateTime.Now.GetFirstDayOfWeek();
        var lastDayOfCurrentWeek = firstDayOfCurrentWeek.AddDays(6);

        var realWorkouts = await _realWorkoutRepository
            .GetAllFromDateRangeAsync(firstDayOfCurrentWeek, lastDayOfCurrentWeek, true, token,
                incl => incl.WorkoutType, incl => incl.Instructor);

        if (!realWorkouts.Any())
            throw new NotFoundException($"Real workouts from current week not found. " +
                                        $"[Date from: {firstDayOfCurrentWeek} to {lastDayOfCurrentWeek}]");

        return _mapper.Map<List<RealWorkoutFromCurrentWeekDto>>(realWorkouts);
    }
}
