using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;

public class UpdateRealWorkoutCommandHandler : IRequestHandler<UpdateRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IMapper _mapper;

    public UpdateRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        IInstructorRepository instructorRepository,
        IWorkoutTypeRepository workoutTypeRepository,
        IMapper mapper)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateRealWorkoutCommand request, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.RealWorkoutId, token);
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

        var dailyWorkoutsList = await _realWorkoutRepository
            .GetAllAsync(request.Date, request.Date.AddDays(1), token);
        
        var validator = new UpdateRealWorkoutCommandValidator(dailyWorkoutsList, realWorkout);
        await validator.ValidateAndThrowAsync(request, token);

        var mappedRealWorkout = _mapper.Map<RealWorkout>(request);
        mappedRealWorkout.WorkoutTypeId = realWorkout.WorkoutType.Id;
        mappedRealWorkout.LastModifiedDate = DateTime.Now;

        await _realWorkoutRepository.UpdateAsync(mappedRealWorkout, token);
        return Unit.Value;
    }
}
