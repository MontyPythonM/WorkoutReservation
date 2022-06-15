using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts
{
    public interface IReservationRepository
    {
        public Task<List<Reservation>> GetReservationsAsyncByGuid(Guid userId);
    }
}
