using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities
{
    public class RepetitiveWorkout : BaseWorkout
    {
        public DayOfWeek DayOfWeek { get; set; }
    }
}
