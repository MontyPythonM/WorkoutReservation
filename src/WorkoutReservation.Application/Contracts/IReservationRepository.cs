using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts
{
    public interface IReservationRepository
    {
        public Task<List<Reservation>> GetUserReservationsByGuidAsync(Guid userId);
        public Task<Reservation> AddReservationAsync(Reservation reservation);
        public Task<bool> CheckUserReservationAsync(int workoutId, Guid currentUserId);
        public Task<Reservation> GetReservationByIdAsync(int reservationId);
        public Task UpdateReservationAsync(Reservation reservation);
    }
}
