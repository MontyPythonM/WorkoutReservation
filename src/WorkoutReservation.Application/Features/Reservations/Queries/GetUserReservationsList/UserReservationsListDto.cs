using System.Text.Json.Serialization;
using WorkoutReservation.Application.Common.Models;
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

        public RealWorkoutDto RealWorkout { get; set; }
    }
}

