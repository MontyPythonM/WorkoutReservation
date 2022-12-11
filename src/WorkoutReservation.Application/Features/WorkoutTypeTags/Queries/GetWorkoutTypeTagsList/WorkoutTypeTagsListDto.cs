namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTagsList;

public class WorkoutTypeTagsListDto
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public bool IsActive { get; set; }
}