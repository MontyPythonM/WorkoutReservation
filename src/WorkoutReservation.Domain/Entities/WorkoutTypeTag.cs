namespace WorkoutReservation.Domain.Entities;

public class WorkoutTypeTag
{
    public int Id { get; set; }
    public string Tag { get; set; }

    public int WorkoutTypeId { get; set; }
    public WorkoutType WorkoutType { get; set; }
}
