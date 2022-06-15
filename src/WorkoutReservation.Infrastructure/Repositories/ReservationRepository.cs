using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _dbContext;

        public ReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetReservationsAsyncByGuid(Guid userId)
        {
            return await _dbContext.Reservations
                .AsNoTracking()
                .Include(x => x.RealWorkout)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.RealWorkout.Date)
                    .ThenBy(x => x.RealWorkout.StartTime)
                .ToListAsync();
        }
    }
}
