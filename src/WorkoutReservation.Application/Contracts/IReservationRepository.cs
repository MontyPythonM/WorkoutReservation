using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IReservationRepository
{
    public Task AddReservationAsync(Reservation reservation, CancellationToken token);
    public Task<bool> CheckIsReservedAsync(RealWorkout realWorkout, ApplicationUser user, CancellationToken token);
    public Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token,
        params Expression<Func<Reservation, object>>[] includes);
    public Task UpdateAsync(Reservation reservation, CancellationToken token);
    public IQueryable<Reservation> GetUserReservationsQuery(Guid userId);
    public Task<Reservation> GetDetailsByIdAsync(int reservationId, Guid userId, bool asNoTracking, CancellationToken token);
}
