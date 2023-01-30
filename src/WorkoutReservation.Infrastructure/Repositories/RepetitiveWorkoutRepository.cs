using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class RepetitiveWorkoutRepository : IRepetitiveWorkoutRepository
{
    private readonly AppDbContext _dbContext;

    public RepetitiveWorkoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RepetitiveWorkout> AddAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(repetitiveWorkout, token);
        await _dbContext.SaveChangesAsync(token);

        return repetitiveWorkout;
    }

    public async Task DeleteAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token)
    {
        _dbContext.Remove(repetitiveWorkout);
        await _dbContext.SaveChangesAsync(token);
    }
    public async Task DeleteAsync(IEnumerable<RepetitiveWorkout> repetitiveWorkouts, CancellationToken token)
    {
        _dbContext.RemoveRange(repetitiveWorkouts);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token)
    {
        _dbContext.Update(repetitiveWorkout);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<List<RepetitiveWorkout>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.RepetitiveWorkouts
            .Include(x => x.Instructor)
            .Include(x => x.WorkoutType)
            .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek, CancellationToken token)
    {
        return await _dbContext.RepetitiveWorkouts
            .AsNoTracking()
            .Include(x => x.Instructor)
            .Include(x => x.WorkoutType)
            .Where(x => x.DayOfWeek == dayOfWeek)
            .OrderBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId, CancellationToken token)
    {
        return await _dbContext.RepetitiveWorkouts
            .AsNoTracking()
            .Include(x => x.Instructor)
            .Include(x => x.WorkoutType)
            .FirstOrDefaultAsync(x => x.Id == repetitiveWorkoutId, token);
    }
}
