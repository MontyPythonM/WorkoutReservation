﻿using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;

public record UpdateRealWorkoutCommand(int Id, int MaxParticipantNumber, DateOnly Date, 
    TimeOnly StartTime, TimeOnly EndTime, int InstructorId) : IRequest;

internal sealed class UpdateRealWorkoutCommandHandler : IRequestHandler<UpdateRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;

    public UpdateRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        IInstructorRepository instructorRepository)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _instructorRepository = instructorRepository;
    }

    public async Task<Unit> Handle(UpdateRealWorkoutCommand request, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.Id, false, token, 
            incl => incl.Instructor, incl => incl.WorkoutType);
        
        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), request.Id.ToString());

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        
        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());
        
        var dailyWorkoutsList = await _realWorkoutRepository.GetByDayAsync(request.Date, true, token);
        
        var validator = new UpdateRealWorkoutCommandValidator(dailyWorkoutsList, realWorkout);
        await validator.ValidateAndThrowAsync(request, token);

        realWorkout.Update(request.MaxParticipantNumber, request.Date, 
            request.StartTime, request.EndTime, instructor);
        
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        return Unit.Value;
    }
}
