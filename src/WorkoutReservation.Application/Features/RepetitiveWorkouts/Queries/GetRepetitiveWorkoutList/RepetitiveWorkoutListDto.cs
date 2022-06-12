using WorkoutReservation.Application.Common.Models;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList
{
    public class RepetitiveWorkoutListDto
    {
        public int Id { get; set; }
        public int MaxParticipianNumber { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public WorkoutTypeDto WorkoutType { get; set; }
        public InstructorDto Instructor { get; set; }
    }
}
