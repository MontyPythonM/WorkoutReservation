using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;

internal sealed class CreateRealWorkoutCommandValidator : AbstractValidator<CreateRealWorkoutCommand>
{
    public CreateRealWorkoutCommandValidator(List<RealWorkout> dailyWorkouts)
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).NotEmpty();
        RuleFor(x => x.InstructorId).NotEmpty();
        RuleFor(x => x.WorkoutTypeId).NotEmpty();

        RuleFor(x => x.MaxParticipantNumber)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();
        
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
    }
}
