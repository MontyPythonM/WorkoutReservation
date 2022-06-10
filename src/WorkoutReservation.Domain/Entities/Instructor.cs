using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities
{
    public class Instructor : AuditableEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }

        public List<WorkoutType> WorkoutTypes { get; set; } = new List<WorkoutType>();
        public List<WorkoutBase> Workouts { get; set; } = new List<WorkoutBase>();
    }
}
