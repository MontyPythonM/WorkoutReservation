using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeactivateWorkoutTypeTag;

public class DeactivateWorkoutTypeTagCommandHandler : IRequestHandler<DeactivateWorkoutTypeTagCommand>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;

    public DeactivateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
    }
    
    public async Task<Unit> Handle(DeactivateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = await _workoutTypeTagRepository.GetByIdAsync(request.Id, false, token);

        if (workoutTypeTag is null)
            throw new NotFoundException($"Workout type with Id: {request.Id} not found.");
        
        workoutTypeTag.IsActive = false;
        await _workoutTypeTagRepository.UpdateAsync(workoutTypeTag, token);
        
        return Unit.Value;
    }
}