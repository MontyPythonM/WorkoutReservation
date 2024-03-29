﻿using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;

internal sealed class UpdateRepetitiveWorkoutCommandValidator : AbstractValidator<UpdateRepetitiveWorkoutCommand>
{
    public UpdateRepetitiveWorkoutCommandValidator(RepetitiveWorkout newRepetitiveWorkout,
        IEnumerable<RepetitiveWorkout> dailyWorkouts)
    {
        RuleFor(x => x.DayOfWeek).IsInEnum();
        RuleFor(x => x.WorkoutTypeId).NotEmpty();
        RuleFor(x => x.InstructorId).NotEmpty();
        
        RuleFor(x => new { x.StartTime, x.EndTime })
            .NotEmpty()
            .Custom((newWorkout, context) =>
            {
                foreach (var existWorkout in dailyWorkouts)
                {                                           
                    // exclude edited workout from list
                    if (existWorkout.Id == newRepetitiveWorkout.Id)
                        continue;

                    // isConflict = internal collision || existing workout time contains new workout endtime || new workout time covers existing 
                    var isConflict = (newWorkout.StartTime >= existWorkout.StartTime && newWorkout.StartTime < existWorkout.EndTime) ||
                                     (newWorkout.EndTime > existWorkout.StartTime && newWorkout.EndTime <= existWorkout.EndTime) ||
                                     (newWorkout.StartTime < existWorkout.StartTime && newWorkout.EndTime > existWorkout.EndTime);

                    if (isConflict)
                    {
                        context.AddFailure("StartTime - EndTime", "The sent workout time conflicts with existing workouts.");
                        break;
                    }
                }
            });
    }
}
