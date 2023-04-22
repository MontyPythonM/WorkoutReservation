using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;

public record GetRealWorkoutDetailQuery(int RealWorkoutId) : IRequest<RealWorkoutDetailDto>;

internal sealed class GetRealWorkoutDetailQueryHandler : IRequestHandler<GetRealWorkoutDetailQuery, RealWorkoutDetailDto>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRealWorkoutDetailQueryHandler(IRealWorkoutRepository realWorkoutRepository,
        IMapper mapper)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<RealWorkoutDetailDto> Handle(GetRealWorkoutDetailQuery request, 
        CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository
            .GetByIdAsync(request.RealWorkoutId, true, token,
                incl => incl.WorkoutType, incl => incl.Instructor);
        
        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), request.RealWorkoutId.ToString());

        return _mapper.Map<RealWorkoutDetailDto>(realWorkout);
    }
}
