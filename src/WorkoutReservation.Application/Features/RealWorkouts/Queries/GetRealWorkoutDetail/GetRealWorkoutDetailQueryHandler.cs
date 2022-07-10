using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;

public class GetRealWorkoutDetailQueryHandler : IRequestHandler<GetRealWorkoutDetailQuery, 
                                                                RealWorkoutDetailDto>
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
                                                   CancellationToken cancellationToken)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.RealWorkoutId);

        if (realWorkout is null)
            throw new NotFoundException($"Workout with Id: {request.RealWorkoutId} not found.");

        return _mapper.Map<RealWorkoutDetailDto>(realWorkout);
    }
}
