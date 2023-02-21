using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;

public record GetRealWorkoutFromUpcomingWeekQuery : IRequest<List<RealWorkoutFromUpcomingWeekDto>>;

internal sealed class GetRealWorkoutFromUpcomingWeekQueryHandler : IRequestHandler<GetRealWorkoutFromUpcomingWeekQuery,
    List<RealWorkoutFromUpcomingWeekDto>>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRealWorkoutFromUpcomingWeekQueryHandler(IRealWorkoutRepository realWorkoutRepository, IMapper mapper)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }
    
    public async Task<List<RealWorkoutFromUpcomingWeekDto>> Handle(GetRealWorkoutFromUpcomingWeekQuery request, 
        CancellationToken token)
    {
        var firstDayOfUpcomingWeek = DateTime.Now.GetFirstDayOfWeekAndAddDays(7);
        var lastDayOfUpcomingWeek = DateTime.Now.GetFirstDayOfWeekAndAddDays(13);
        
        var realWorkouts = await _realWorkoutRepository
            .GetAllFromDateRangeAsync(firstDayOfUpcomingWeek, lastDayOfUpcomingWeek, true, token,
                incl => incl.WorkoutType, incl => incl.Instructor);
        
        return _mapper.Map<List<RealWorkoutFromUpcomingWeekDto>>(realWorkouts);
    }
}
