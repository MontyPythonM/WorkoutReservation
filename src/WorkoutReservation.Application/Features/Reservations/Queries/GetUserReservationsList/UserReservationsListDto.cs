using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public class UserReservationsListDto
{
    public int Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReservationStatus ReservationStatus { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string WorkoutTypeName { get; set; }
    public string InstructorFullName { get; set; }
}

