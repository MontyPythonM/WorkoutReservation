namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;

public class ActiveWorkoutTypeTagsDto
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; }
}