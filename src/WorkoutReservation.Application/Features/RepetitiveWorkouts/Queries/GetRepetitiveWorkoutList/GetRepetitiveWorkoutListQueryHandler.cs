using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

public record GetRepetitiveWorkoutListQuery : IRequest<List<RepetitiveWorkoutListDto>>;

internal sealed class GetRepetitiveWorkoutListQueryHandler : IRequestHandler<GetRepetitiveWorkoutListQuery, 
    List<RepetitiveWorkoutListDto>>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRepetitiveWorkoutListQueryHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository, 
        IMapper mapper)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<List<RepetitiveWorkoutListDto>> Handle(GetRepetitiveWorkoutListQuery request, 
        CancellationToken token)
    {
        var repetitiveWorkouts = await _repetitiveWorkoutRepository.GetAllAsync(token);
        return _mapper.Map<List<RepetitiveWorkoutListDto>>(repetitiveWorkouts);
    }
}
