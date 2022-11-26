using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IReservationRepository
{
    public Task<List<Reservation>> GetUserReservationsByGuidAsync(Guid userId, CancellationToken token);
    public Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken token);
    public Task<bool> CheckUserReservationAsync(int workoutId, Guid currentUserId, CancellationToken token);
    public Task<Reservation> GetReservationByIdAsync(int reservationId, CancellationToken token);
    public Task UpdateReservationAsync(Reservation reservation, CancellationToken token);
    public IQueryable<Reservation> GetUserReservationsByGuidQuery(Guid userId);
}
