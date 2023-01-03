﻿using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

public class GenerateUpcomingWorkoutTimetableCommandValidator : AbstractValidator<GenerateUpcomingWorkoutTimetableCommand>
{
    public GenerateUpcomingWorkoutTimetableCommandValidator(List<RealWorkout> realWorkouts)
    {
        RuleFor(x => x.IsAutoGenerated)
            .NotNull();

        RuleFor(x => x).Custom((value, context) =>
        {
            if (realWorkouts.Any(x => x.InstructorId.Equals(default) || x.WorkoutTypeId.Equals(default)))
            {
                context.AddFailure($"At least one workout does not have an assigned workout type or instructor.");
            }
        });
    }
}
