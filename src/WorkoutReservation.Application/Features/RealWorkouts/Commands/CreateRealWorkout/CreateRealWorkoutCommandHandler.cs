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

    public CreateRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        IInstructorRepository instructorRepository, IWorkoutTypeRepository workoutTypeRepository)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
    }

    public async Task<Unit> Handle(CreateRealWorkoutCommand request, CancellationToken token)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());

        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, false, token);
        if (workoutType is null)
            throw new NotFoundException(nameof(WorkoutType), request.InstructorId.ToString());

        var dailyWorkoutsList = await _realWorkoutRepository.GetByDayAsync(request.Date, true, token);
        
        var validator = new CreateRealWorkoutCommandValidator(dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        var realWorkout = new RealWorkout(request.MaxParticipantNumber, request.StartTime,
            request.EndTime, workoutType, instructor, request.Date, false);
        
        await _realWorkoutRepository.AddAsync(realWorkout, token);
        return Unit.Value;
    }
}
