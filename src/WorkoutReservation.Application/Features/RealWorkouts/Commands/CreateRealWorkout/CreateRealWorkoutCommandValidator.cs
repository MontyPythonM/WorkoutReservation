using FluentValidation;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;

public class CreateRealWorkoutCommandValidator : AbstractValidator<CreateRealWorkoutCommand>
{
    public CreateRealWorkoutCommandValidator(List<RealWorkout> dailyWorkouts)
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .Custom((value, context) => 
            {
                if (value < DateOnly.FromDateTime(DateTime.Now))
                {
                    context.AddFailure("You cannot create a workout with a past date.");
                }

                if (value > DateTime.Now.GetFirstDayOfWeek().AddDays(13))
                {
                    context.AddFailure("You cannot create a workout with a date further than 2 weeks (counting from the beginning of the current week).");
                }
            });

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("'StartTime' must be earlier than the 'EndTime'.");

        RuleFor(x => new { x.StartTime, x.EndTime })
            .NotEmpty()
            .Custom((newWorkout, context) =>
            {
                foreach (var existWorkout in dailyWorkouts)
                {
                    // isConflict = internal collision OR existing workout time contains new workout endtime OR new workout time covers existing 
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

        RuleFor(x => x.MaxParticipantNumber)
            .GreaterThan(0)
            .NotNull();
    }
}
