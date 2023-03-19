using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class RepetitiveWorkoutRepository : IRepetitiveWorkoutRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<RepetitiveWorkout> _repository;

    public RepetitiveWorkoutRepository(AppDbContext dbContext, 
        IRepository<RepetitiveWorkout> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
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
        var query = _dbContext.RepetitiveWorkouts.AsQueryable();
            
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query); 
            
        return await query
            .OrderBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek, bool asNoTracking, 
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes)
    {
        var query = _dbContext.RepetitiveWorkouts.AsQueryable();
            
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query); 
            
        return await query
            .Where(x => x.DayOfWeek == dayOfWeek)
            .OrderBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes)
    {
        var query = _dbContext.RepetitiveWorkouts.AsQueryable();
            
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query); 

        return await query.FirstOrDefaultAsync(x => x.Id == repetitiveWorkoutId, token);
    }
}
