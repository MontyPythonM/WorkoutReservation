using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType
{
    public class UpdateWorkoutTypeCommandHandler : IRequestHandler<UpdateWorkoutTypeCommand>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        private readonly IMapper _mapper;

        public UpdateWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository, IMapper mapper)
        {
            _workoutTypeRepository = workoutTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWorkoutTypeCommand request, CancellationToken cancellationToken)
        {
            var workoutType = await _workoutTypeRepository.GetByIdAsync(request.Id);

            if (workoutType is null)
                throw new NotFoundException($"Workout type with Id: {request.Id} not found.");

            var mappedWorkoutType = _mapper.Map<WorkoutType>(request);

            await _workoutTypeRepository.UpdateAsync(mappedWorkoutType);

            return Unit.Value;
        }
    }
}
