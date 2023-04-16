using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

public class RepetitiveWorkoutListDto
{
    public int Id { get; set; }
    public int MaxParticipantNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public int WorkoutTypeId { get; set; }
    public string WorkoutTypeName { get; set; }
    public WorkoutIntensity WorkoutTypeIntensity { get; set; }
    public int InstructorId { get; set; }
    public string InstructorShortName { get; set; }
    public string InstructorEmail { get; set; }
    public bool IsRealWorkoutConflict { get; set; }
}
