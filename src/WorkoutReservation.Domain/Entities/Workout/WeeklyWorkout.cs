using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities.Workout
{
    public class WeeklyWorkout : WorkoutBase
    {
        public DayOfWeek DayOfWeek { get; set; }

    }
}
