using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public record CreateWorkoutTypeTagCommand(string Tag) : IRequest;

internal sealed class CreateWorkoutTypeTagCommandHandler : IRequestHandler<CreateWorkoutTypeTagCommand>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public CreateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, 
        ICurrentUserAccessor currentUserAccessor)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<Unit> Handle(CreateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = new WorkoutTypeTag(request.Tag, _currentUserAccessor.GetUserId());
        
        var validator = new CreateWorkoutTypeTagCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        await _workoutTypeTagRepository.AddAsync(workoutTypeTag, token);
        return Unit.Value;
    }
}