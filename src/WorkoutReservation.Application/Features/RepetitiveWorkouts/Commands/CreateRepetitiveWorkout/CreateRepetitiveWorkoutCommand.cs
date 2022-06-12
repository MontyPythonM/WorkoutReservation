using MediatR;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout
{
    public class CreateRepetitiveWorkoutCommand : IRequest<int>
    {
        public int MaxParticipianNumber { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int WorkoutTypeId { get; set; }
        public int InstructorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
