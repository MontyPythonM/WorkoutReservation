using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities.Workout
{
    public class WeeklyWorkout : BaseWorkout
    {
        public DayOfWeek DayOfWeek { get; set; }

    }
}
