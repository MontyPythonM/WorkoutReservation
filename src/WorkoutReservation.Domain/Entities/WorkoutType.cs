using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities
{
    public class WorkoutType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkoutIntensity Intensity { get; set; }
        
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
        public List<WorkoutTypeTag> WorkoutTypeTags { get; set; } = new List<WorkoutTypeTag>();
        public List<BaseWorkout> BaseWorkouts { get; set; } = new List<BaseWorkout>();
    }
}
