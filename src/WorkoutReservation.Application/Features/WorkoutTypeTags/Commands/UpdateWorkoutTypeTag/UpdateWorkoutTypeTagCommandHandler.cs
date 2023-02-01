using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.UpdateWorkoutTypeTag;

public record UpdateWorkoutTypeTagCommand(int Id, string Tag, bool IsActive) : IRequest;

internal sealed class UpdateWorkoutTypeTagCommandHandler : IRequestHandler<UpdateWorkoutTypeTagCommand>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    
    public UpdateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
    }
    
    public async Task<Unit> Handle(UpdateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = await _workoutTypeTagRepository.GetByIdAsync(request.Id, false, token);
        
        if (workoutTypeTag is null)
            throw new NotFoundException($"Workout type tag with Id: {request.Id} not found.");
        
        var validator = new UpdateWorkoutTypeTagCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);

        workoutTypeTag.Tag = request.Tag;
        workoutTypeTag.IsActive = request.IsActive;
        
        await _workoutTypeTagRepository.UpdateAsync(workoutTypeTag, token);
        return Unit.Value;
    }
}