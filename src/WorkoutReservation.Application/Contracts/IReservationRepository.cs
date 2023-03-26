using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IReservationRepository
{
    public Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token,
        params Expression<Func<Reservation, object>>[] includes);
    public Task<Reservation> GetDetailsByIdAsync(int reservationId, bool asNoTracking, CancellationToken token);
    public Task<(List<Reservation> reservations, int totalItems)> GetPagedAsync(IPagedQuery request,
        Guid userId, CancellationToken token);
    public Task<(List<Reservation> reservations, int totalItems)> GetPagedAsync(IPagedQuery request,
        CancellationToken token);
}
