using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail
{
    public class GetWorkoutTypeDetailQuery : IRequest<WorkoutTypeDetailDto>
    {
        public int WorkoutTypeId { get; set; }
    }

    public class GetWorkoutTypeDetailQueryHandler : IRequestHandler<GetWorkoutTypeDetailQuery, WorkoutTypeDetailDto>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        private readonly IMapper _mapper;

        public GetWorkoutTypeDetailQueryHandler(IWorkoutTypeRepository workoutTypeRepository, IMapper mapper)
        {
            _workoutTypeRepository = workoutTypeRepository;
            _mapper = mapper;
        }

        public async Task<WorkoutTypeDetailDto> Handle(GetWorkoutTypeDetailQuery request, CancellationToken cancellationToken)
        {
            var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId);

            if (workoutType is null)
                throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

            var result = _mapper.Map<WorkoutTypeDetailDto>(workoutType);

            return result;
        }
    }
}
