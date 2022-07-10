using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;

public class WorkoutTypeDetailQueryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public WorkoutIntensity Intensity { get; set; }

    public List<InstructorForWorkoutTypeDetailDto> Instructors { get; set; }
    public List<WorkoutTypeTagForWorkoutTypeDetailDto> WorkoutTypeTags { get; set; }
}

public class InstructorForWorkoutTypeDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class WorkoutTypeTagForWorkoutTypeDetailDto
{
    public int Id { get; set; }
    public string Tag { get; set; }
}
