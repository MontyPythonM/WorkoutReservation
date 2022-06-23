using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts
{
    public interface IReservationRepository
    {
        public Task<List<Reservation>> GetUserReservationsAsyncByGuid(Guid userId);
        public Task<Reservation> AddReservation(Reservation reservation);
        public Task<bool> CheckUserAlreadyReservedWorkout(int workoutId, Guid currentUserId);
    }
}
