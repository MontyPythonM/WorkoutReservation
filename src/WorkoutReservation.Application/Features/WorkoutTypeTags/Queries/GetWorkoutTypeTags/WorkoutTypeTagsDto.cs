namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;

public class WorkoutTypeTagsDto
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; }
    public int NumberOfUses { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}