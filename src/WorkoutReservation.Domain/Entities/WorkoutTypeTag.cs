namespace WorkoutReservation.Domain.Entities;

public class WorkoutTypeTag
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<WorkoutType> WorkoutTypes { get; set; } = new List<WorkoutType>();

    protected WorkoutTypeTag()
    {
        // for EF
    }
    
    public WorkoutTypeTag(string tag, Guid userId)
    {
        Tag = tag;
        IsActive = true;
        CreatedDate = DateTime.Now;
        CreatedBy = userId.ToString();
    }
}
