namespace WorkoutReservation.Domain.Entities
{
    public class WorkoutTypeInstructor
    {
        public int WorkoutTypeId { get; set; }
        public WorkoutType WorkoutType { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
