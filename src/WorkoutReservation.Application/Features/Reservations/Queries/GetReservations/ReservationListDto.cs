using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public class ReservationListDto
{
    public int Id { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public string Note { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerFullName { get; set; }
    
    public int RealWorkoutId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public bool IsWorkoutExpired { get; set; }
    public int MaxParticipantNumber { get; set; }
    public int CurrentParticipantNumber { get; set; }
    
    public int WorkoutTypeId { get; set; }
    public string WorkoutTypeName { get; set; }
    
    public int InstructorId { get; set; }
    public string InstructorFullName { get; set; }
}