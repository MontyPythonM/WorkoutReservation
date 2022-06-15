namespace WorkoutReservation.Application.Common.Models
{
    public class RealWorkoutDto
    {
        public int Id { get; set; }
        public int MaxParticipianNumber { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public WorkoutTypeDto WorkoutType { get; set; }
        public InstructorDto Instructor { get; set; }

        public DateOnly Date { get; set; }
        public int CurrentParticipianNumber { get; set; }
    }
}
