namespace WorkoutReservation.Domain.Entities;

public class WorkoutTypeTag
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<WorkoutType> WorkoutTypes { get; set; } = new List<WorkoutType>();
}
