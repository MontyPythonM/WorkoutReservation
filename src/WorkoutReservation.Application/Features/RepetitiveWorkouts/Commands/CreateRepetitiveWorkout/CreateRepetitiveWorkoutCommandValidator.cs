using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;

public class CreateRepetitiveWorkoutCommandValidator : AbstractValidator<CreateRepetitiveWorkoutCommand>
{
    public CreateRepetitiveWorkoutCommandValidator(List<RepetitiveWorkout> dailyExistWorkouts)
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum();

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime);

        RuleFor(x => new { x.StartTime, x.EndTime })
            .NotEmpty()
            .Custom((newWorkout, context) => 
            {
                foreach (var existWorkout in dailyExistWorkouts)
                {
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

        RuleFor(x => x.MaxParticipiantNumber)
            .GreaterThan(0)
            .NotNull();
    }
}
