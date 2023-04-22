using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

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
            throw new NotFoundException(nameof(WorkoutTypeTag), request.Id.ToString());
        
        workoutTypeTag.Update(request.Tag, request.IsActive);
        
        await _workoutTypeTagRepository.UpdateAsync(workoutTypeTag, token);
        return Unit.Value;
    }
}