namespace WorkoutReservation.Domain.Entities;

public sealed class WorkoutTypeInstructor
{
    public int WorkoutTypeId { get; private set; }
    public WorkoutType WorkoutType { get; private set; }

    public int InstructorId { get; private set; }
    public Instructor Instructor { get; private set; }

    private WorkoutTypeInstructor()
    {
        // required for EF Core
    }
    
    public WorkoutTypeInstructor(int workoutTypeId, int instructorId)
    {
        WorkoutTypeId = workoutTypeId;
        InstructorId = instructorId;
    }
}
