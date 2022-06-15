using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList
{
    public class UserReservationsListDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationStatus ReservationStatus { get; set; }

        public RealWorkoutForUserReservationsListDto RealWorkout { get; set; }
    }

    public class RealWorkoutForUserReservationsListDto
    {
        public int Id { get; set; }
        public int MaxParticipianNumber { get; set; }
        public int CurrentParticipianNumber { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }

        public WorkoutTypeForUserReservationsListDto WorkoutType { get; set; }
        public InstructorForUserReservationsListDto Instructor { get; set; }
    }

    public class WorkoutTypeForUserReservationsListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkoutIntensity Intensity { get; set; }
    }

    public class InstructorForUserReservationsListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

