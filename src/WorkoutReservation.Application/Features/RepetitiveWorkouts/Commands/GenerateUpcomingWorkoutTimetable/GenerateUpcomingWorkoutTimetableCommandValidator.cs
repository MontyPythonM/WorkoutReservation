﻿using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

internal sealed class GenerateUpcomingWorkoutTimetableCommandValidator : AbstractValidator<GenerateUpcomingWorkoutTimetableCommand>
{
    public GenerateUpcomingWorkoutTimetableCommandValidator(IReadOnlyCollection<RealWorkout> newRealWorkouts, 
        IReadOnlyCollection<RealWorkout> existingRealWorkouts)
    {
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

            foreach (var newWorkout in newRealWorkouts.Select(n => new { n.StartTime, n.EndTime, n.Date }))
            {
                foreach (var existingWorkout in existingRealWorkouts.Select(e => new { e.StartTime, e.EndTime, e.Date }))
                {
                    var isCollision = (newWorkout.Date == existingWorkout.Date) && (
                        (newWorkout.StartTime >= existingWorkout.StartTime && newWorkout.StartTime < existingWorkout.EndTime) ||
                        (newWorkout.EndTime > existingWorkout.StartTime && newWorkout.EndTime <= existingWorkout.EndTime) ||
                        (newWorkout.StartTime < existingWorkout.StartTime && newWorkout.EndTime > existingWorkout.EndTime));

                    if (isCollision)
                    {
                        context.AddFailure($"Time conflict between new generated and existing workouts. " +
                                           $"Collision new workout details: {newWorkout.Date}, {newWorkout.StartTime} - {newWorkout.EndTime}].");
                    }
                }
            }
        });
    }
}
