using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail
{
    public class InstructorDetailQueryDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }

        public List<WorkoutTypesDto> WorkoutTypes { get; set; } = new List<WorkoutTypesDto>();
    }

    public class WorkoutTypesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkoutIntensity Intensity { get; set; }
    }

}
