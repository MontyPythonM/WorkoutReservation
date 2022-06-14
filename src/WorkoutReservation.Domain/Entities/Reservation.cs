using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public RealWorkout RealWorkout { get; set; }
        public int RealWorkoutId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
