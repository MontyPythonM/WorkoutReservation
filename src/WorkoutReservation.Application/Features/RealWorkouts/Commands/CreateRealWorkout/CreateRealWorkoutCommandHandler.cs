using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;

public record CreateRealWorkoutCommand(int MaxParticipantNumber, DateOnly Date, TimeOnly StartTime, 
    TimeOnly EndTime, int WorkoutTypeId, int InstructorId) : IRequest;

internal sealed class CreateRealWorkoutCommandHandler : IRequestHandler<CreateRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;    

    public CreateRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        IInstructorRepository instructorRepository, IWorkoutTypeRepository workoutTypeRepository, 
        ICurrentUserAccessor currentUserAccessor)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<Unit> Handle(CreateRealWorkoutCommand request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, true, token);
        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

        var dailyWorkoutsList = await _realWorkoutRepository.GetByDayAsync(request.Date, true, token);
        var validator = new CreateRealWorkoutCommandValidator(dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        var realWorkout = new RealWorkout(request.MaxParticipantNumber, request.StartTime, 
            request.EndTime, workoutType, instructor, request.Date, user);
        
        await _realWorkoutRepository.AddAsync(realWorkout, token);
        return Unit.Value;
    }
}
