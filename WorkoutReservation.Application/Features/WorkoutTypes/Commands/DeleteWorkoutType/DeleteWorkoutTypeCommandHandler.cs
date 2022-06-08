using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType
{
    public class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;

        public DeleteWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository)
        {
            _workoutTypeRepository = workoutTypeRepository;
        }

        public async Task<Unit> Handle(DeleteWorkoutTypeCommand request, CancellationToken cancellationToken)
        {
            var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId);

            if (workoutType is null)
                throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

            await _workoutTypeRepository.DeleteAsync(workoutType);

            return Unit.Value;
        }
    }
}
