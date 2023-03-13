using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;

internal sealed class UpdateRealWorkoutCommandValidator : AbstractValidator<UpdateRealWorkoutCommand>
{
    public UpdateRealWorkoutCommandValidator(List<RealWorkout> dailyWorkouts, RealWorkout editedRealWorkout)
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (value < DateOnly.FromDateTime(DateTime.Now))
                {
                    context.AddFailure("You cannot update a workout with a past date");
                }
            });

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("'StartTime' must be earlier than the 'EndTime'");

        RuleFor(x => new { x.StartTime, x.EndTime })
            .NotEmpty()
            .Custom((newWorkout, context) =>
            {
                foreach (var existWorkout in dailyWorkouts)
                {                        
                    // exclude edited workout from list
                    if (existWorkout.Id == editedRealWorkout.Id)
                        continue;

                    // isConflict = internal collision OR existing workout time contains new workout endtime OR new workout time covers existing 
                    var isConflict = (newWorkout.StartTime >= existWorkout.StartTime && newWorkout.StartTime < existWorkout.EndTime) ||
                                     (newWorkout.EndTime > existWorkout.StartTime && newWorkout.EndTime <= existWorkout.EndTime) ||
                                     (newWorkout.StartTime < existWorkout.StartTime && newWorkout.EndTime > existWorkout.EndTime);

                    if (isConflict)
                    {
                        context.AddFailure("StartTime - EndTime", "The sent workout time conflicts with existing workouts");
                        break;
                    }
                }
            });

        RuleFor(x => x.MaxParticipantNumber)
            .GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(editedRealWorkout.CurrentParticipantNumber)
            .NotNull();
    }
}
