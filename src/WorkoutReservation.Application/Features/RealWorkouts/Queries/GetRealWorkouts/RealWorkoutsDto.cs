using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkouts;

public class RealWorkoutDto
{
    public int Id { get; set; }
    public int CurrentParticipantNumber { get; set; }
    public int MaxParticipantNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsExpired { get; set; }
    public int? ReservationId { get; set; }
    public bool IsAlreadyReserved { get; set; }

    public int WorkoutTypeId { get; set; }
    public string WorkoutTypeName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WorkoutIntensity WorkoutIntensity { get; set; }
    
    public int InstructorId { get; set; }
    public string InstructorFullName { get; set; }
    public string InstructorShortName { get; set; }
}
