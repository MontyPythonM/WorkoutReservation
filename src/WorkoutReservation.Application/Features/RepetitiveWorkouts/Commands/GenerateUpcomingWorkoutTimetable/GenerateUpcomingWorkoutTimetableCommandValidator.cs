﻿using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

public class GenerateUpcomingWorkoutTimetableCommandValidator : AbstractValidator<GenerateUpcomingWorkoutTimetableCommand>
{
    public GenerateUpcomingWorkoutTimetableCommandValidator(List<RealWorkout> newRealWorkouts, 
        List<RealWorkout> existingRealWorkouts)
    {
        RuleFor(x => x.IsAutoGenerated).NotNull();

        RuleFor(x => x).Custom((value, context) =>
        {
            if (newRealWorkouts.Any(x => x.InstructorId.Equals(null) || x.InstructorId.Equals(0) ||
                                         x.WorkoutTypeId.Equals(null) || x.WorkoutTypeId.Equals(0)))
            {
                context.AddFailure("At least one workout does not have an assigned workout type or instructor.");
            }

            if (newRealWorkouts.Any(workout => workout.StartTime > workout.EndTime))
            {
                context.AddFailure("StartTime must be earlier than the EndTime.");
            }

            if (newRealWorkouts.Any(workout => workout.StartTime.ToString().IsNullOrEmpty()))
            {
                context.AddFailure("StartTime", "StartTime cannot be null or empty.");
            }

            if (newRealWorkouts.Any(workout => workout.EndTime.ToString().IsNullOrEmpty()))
            {
                context.AddFailure("EndTime", "EndTime cannot be null or empty.");
            }

            if (newRealWorkouts.Any(workout => workout.MaxParticipantNumber <= 0))
            {
                context.AddFailure("MaxParticipantNumber", "MaxParticipantNumber must be greater than zero.");
            }

            // todo: compare 2 collection of realWorkouts and if any element has the same time range then throw
        });
    }
}
