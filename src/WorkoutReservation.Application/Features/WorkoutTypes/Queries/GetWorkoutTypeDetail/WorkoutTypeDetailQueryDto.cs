using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail
{
    public class WorkoutTypeDetailQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public WorkoutIntensity Intensity { get; set; }

        public List<InstructorDto> Instructors { get; set; }
        public List<WorkoutTypeTagDto> WorkoutTypeTags { get; set; }
    }

    public class WorkoutTypeTagDto
    {
        public int Id { get; set; }
        public string Tag { get; set; }
    }
    public class InstructorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

}
