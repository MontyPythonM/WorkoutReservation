using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Methods;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;

public class GetRealWorkoutFromUpcomingWeekQueryHandler : IRequestHandler<GetRealWorkoutFromUpcomingWeekQuery,
                                                                          List<RealWorkoutFromUpcomingWeekDto>>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRealWorkoutFromUpcomingWeekQueryHandler(IRealWorkoutRepository realWorkoutRepository,
                                                      IMapper mapper)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }
    public async Task<List<RealWorkoutFromUpcomingWeekDto>> Handle(GetRealWorkoutFromUpcomingWeekQuery request, 
                                                                   CancellationToken cancellationToken)
    {
        var firstDayOfCurrentWeek = DateTime.Now.GetFirstDayOfWeek().AddDays(7);
        var lastDayOfCurrentWeek = firstDayOfCurrentWeek.AddDays(7);

        var realWorkouts = await _realWorkoutRepository.GetAllAsync(firstDayOfCurrentWeek, lastDayOfCurrentWeek, cancellationToken);

        if (!realWorkouts.Any())
            throw new NotFoundException($"Real workouts from current week not found. [Date from: {firstDayOfCurrentWeek} to {lastDayOfCurrentWeek.AddDays(-1)}]");

        return _mapper.Map<List<RealWorkoutFromUpcomingWeekDto>>(realWorkouts);
    }
}
