using System.Linq.Expressions;
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

    public async Task AddAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(repetitiveWorkout, token);
        await _dbContext.SaveChangesAsync(token);
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

    public async Task<List<RepetitiveWorkout>> GetAllAsync(bool asNoTracking, 
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes)
    {
        return await _dbContext.RepetitiveWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .OrderBy(r => r.StartTime)
            .ToListAsync(token);
    }

    public async Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek, bool asNoTracking, 
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes)
    {
        return await _dbContext.RepetitiveWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .Where(r => r.DayOfWeek == dayOfWeek)
            .OrderBy(r => r.StartTime)
            .ToListAsync(token);
    }

    public async Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes)
    {
        return await _dbContext.RepetitiveWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(r => r.Id == repetitiveWorkoutId, token);
    }
}
