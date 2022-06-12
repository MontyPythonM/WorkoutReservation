using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities.Workout
{
    public class RepetitiveWorkout : BaseWorkout
    {
        public DayOfWeek DayOfWeek { get; set; }
    }
}
