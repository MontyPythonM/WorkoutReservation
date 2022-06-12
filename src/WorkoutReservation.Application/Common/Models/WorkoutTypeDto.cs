using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Common.Models
{
    public class WorkoutTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkoutIntensity Intensity { get; set; }
    }
}
