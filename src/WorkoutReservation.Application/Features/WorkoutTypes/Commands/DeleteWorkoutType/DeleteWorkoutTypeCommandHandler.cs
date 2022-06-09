using MediatR;
using NLog;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType
{
    public class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        private readonly ILogger _logger;

        public DeleteWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository, ILogger logger)
        {
            _workoutTypeRepository = workoutTypeRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteWorkoutTypeCommand request, 
                                       CancellationToken cancellationToken)
        {
            var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId);

            if (workoutType is null)
                throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

            await _workoutTypeRepository.DeleteAsync(workoutType);

            _logger.Info($"WorkoutType with Id: {request.WorkoutTypeId} was deleted.");

            return Unit.Value;
        }
    }
}
