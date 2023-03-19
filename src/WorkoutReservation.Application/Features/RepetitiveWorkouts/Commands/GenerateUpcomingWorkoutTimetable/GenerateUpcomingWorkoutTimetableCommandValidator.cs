using FluentValidation;
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
                                           $"Collision new workout details: {newWorkout.Date}, {newWorkout.StartTime} - {newWorkout.EndTime}]");
                    }
                }
            }
        });
    }
}
