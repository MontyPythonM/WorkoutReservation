using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities.Workout;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories
{
    public class RealWorkoutRepository : IRealWorkoutRepository
    {
        private readonly AppDbContext _dbContext;

        public RealWorkoutRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RealWorkout>> GetAllAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _dbContext.RealWorkouts
                .AsNoTracking()
                .Include(x => x.Instructor)
                .Include(x => x.WorkoutType)
                .Where(x => x.Date >= startDate && x.Date < endDate)
                .OrderBy(x => x.Date)
                    .ThenBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<RealWorkout> GetByIdAsync(int realworkoutId)
        {
            return await _dbContext.RealWorkouts
                .AsNoTracking()
                .Include(x => x.Instructor)
                .Include(x => x.WorkoutType)
                .FirstOrDefaultAsync(x => x.Id == realworkoutId);             
        }


    }
}
