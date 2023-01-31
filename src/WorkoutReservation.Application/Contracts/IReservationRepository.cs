using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IReservationRepository
{
    public Task<List<Reservation>> GetUserReservationsByGuidAsync(Guid userId, CancellationToken token);
    public Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken token);
    public Task<bool> CheckIsUserReservedAsync(RealWorkout realWorkout, ApplicationUser currentUser, CancellationToken token);
    public Task<Reservation> GetReservationByIdAsync(int reservationId, CancellationToken token);
    public Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token);
    public Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token,
        params Expression<Func<Reservation, object>>[] includes);
    public Task UpdateAsync(Reservation reservation, CancellationToken token);
    public IQueryable<Reservation> GetUserReservationsByGuidQuery(Guid userId);
}
