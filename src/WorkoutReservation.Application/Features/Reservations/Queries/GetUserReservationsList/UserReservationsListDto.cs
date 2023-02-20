using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public class UserReservationsListDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastModificationDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReservationStatus ReservationStatus { get; set; }

    public int RealWorkoutId { get; set; }
    public int MaxParticipantNumber { get; set; }
    public int CurrentParticipantNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    
    public int WorkoutTypeId { get; set; }
    public string WorkoutTypeName { get; set; }
    public WorkoutIntensity Intensity { get; set; }
    
    public int InstructorId { get; set; }
    public string InstructorFullName { get; set; }
}

