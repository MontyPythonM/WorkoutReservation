using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;

public class UpdateRepetitiveWorkoutCommandHandler : IRequestHandler<UpdateRepetitiveWorkoutCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IMapper _mapper;

    public UpdateRepetitiveWorkoutCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
        IInstructorRepository instructorRepository,
        IWorkoutTypeRepository workoutTypeRepository,
        IMapper mapper)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _instructorRepository = instructorRepository; 
        _workoutTypeRepository = workoutTypeRepository;
        _mapper = mapper;
    }

    public IInstructorRepository InstructorRepository => _instructorRepository;

    public async Task<Unit> Handle(UpdateRepetitiveWorkoutCommand request, CancellationToken token)
    {
        var repetitiveWorkout = await _repetitiveWorkoutRepository.GetByIdAsync(request.RepetitiveWorkoutId, token);
        if (repetitiveWorkout is null)
            throw new NotFoundException($"Repetitive workout with Id: {request.RepetitiveWorkoutId} not found.");

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

        var dailyWorkoutsList = await _repetitiveWorkoutRepository
            .GetAllFromSelectedDayAsync(request.DayOfWeek, token);
        
        var validator = new UpdateRepetitiveWorkoutCommandValidator(repetitiveWorkout, dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        var mappedRepetitiveWorkout = _mapper.Map<RepetitiveWorkout>(request);
        mappedRepetitiveWorkout.WorkoutTypeId = repetitiveWorkout.WorkoutType.Id;
        mappedRepetitiveWorkout.LastModifiedDate = DateTime.Now;

        await _repetitiveWorkoutRepository.UpdateAsync(mappedRepetitiveWorkout, token);
        return Unit.Value;
    }
}
