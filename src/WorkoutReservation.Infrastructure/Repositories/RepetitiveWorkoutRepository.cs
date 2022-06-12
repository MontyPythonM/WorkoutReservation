using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities.Workout;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories
{
    public class RepetitiveWorkoutRepository : IRepetitiveWorkoutRepository
    {
        private readonly AppDbContext _dbContext;

        public RepetitiveWorkoutRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepetitiveWorkout> AddAsync(RepetitiveWorkout repetitiveWorkout)
        {
            await _dbContext.AddAsync(repetitiveWorkout);
            await _dbContext.SaveChangesAsync();

            return repetitiveWorkout;
        }

        public async Task DeleteAsync(RepetitiveWorkout repetitiveWorkout)
        {
            _dbContext.Remove(repetitiveWorkout);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAllAsync(List<RepetitiveWorkout> repetitiveWorkouts)
        {
            _dbContext.RemoveRange(repetitiveWorkouts);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RepetitiveWorkout repetitiveWorkout)
        {
            _dbContext.Update(repetitiveWorkout);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<RepetitiveWorkout>> GetAllAsync()
        {
            return await _dbContext.RepetitiveWorkouts
                .Include(x => x.Instructor)
                .Include(x => x.WorkoutType)
                .OrderBy(x => x.DayOfWeek)
                    .ThenBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek)
        {
            return await _dbContext.RepetitiveWorkouts
                .AsNoTracking()
                .Include(x => x.Instructor)
                .Include(x => x.WorkoutType)
                .Where(x => x.DayOfWeek == dayOfWeek)
                .OrderBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId)
        {
            return await _dbContext.RepetitiveWorkouts
                .AsNoTracking()
                .Include(x => x.Instructor)
                .Include(x => x.WorkoutType)
                .FirstOrDefaultAsync(x => x.Id == repetitiveWorkoutId);
        }
    }
}
