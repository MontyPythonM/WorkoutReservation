using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType
{
    public class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        private readonly ILogger<DeleteWorkoutTypeCommandHandler> _logger;

        public DeleteWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository, 
                                               ILogger<DeleteWorkoutTypeCommandHandler> logger)
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

            _logger.LogInformation($"WorkoutType with Id: {request.WorkoutTypeId} was deleted.");

            return Unit.Value;
        }
    }
}
