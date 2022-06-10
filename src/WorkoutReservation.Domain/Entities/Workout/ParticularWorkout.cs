using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities.Workout
{
    public class ParticularWorkout : WorkoutBase
    {
        public DateOnly Date { get; set; }
        public int CurrentParticipianNumber { get; set; }
    }
}
