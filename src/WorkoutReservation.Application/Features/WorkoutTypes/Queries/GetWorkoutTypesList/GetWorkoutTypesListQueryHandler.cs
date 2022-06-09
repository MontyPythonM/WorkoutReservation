using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList
{
    public class GetWorkoutTypesListQueryHandler : IRequestHandler<GetWorkoutTypesListQuery, List<WorkoutTypesListDto>>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        private readonly IMapper _mapper;

        public GetWorkoutTypesListQueryHandler(IWorkoutTypeRepository workoutTypeRepository, IMapper mapper)
        {
            _workoutTypeRepository = workoutTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkoutTypesListDto>> Handle(GetWorkoutTypesListQuery request, CancellationToken cancellationToken)
        {
            var workoutTypes = await _workoutTypeRepository.GetAllAsync();

            if (!workoutTypes.Any())
                throw new NotFoundException($"Workout types not found.");

            return _mapper.Map<List<WorkoutTypesListDto>>(workoutTypes);
        }
    }
}
