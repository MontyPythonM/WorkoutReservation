using System.Text.Json.Serialization;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

public class WorkoutTypesListQueryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WorkoutIntensity Intensity { get; set; }
    public List<WorkoutTypeTagForWorkoutTypeDto> WorkoutTypeTags { get; set; }
    public List<InstructorForWorkoutTypeDto> Instructors { get; set; }
}

public record WorkoutTypeTagForWorkoutTypeDto(int Id, string Tag, bool IsActive);
public record InstructorForWorkoutTypeDto(int Id, string FirstName, string LastName);