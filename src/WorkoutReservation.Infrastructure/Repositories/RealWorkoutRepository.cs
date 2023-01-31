using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class RealWorkoutRepository : IRealWorkoutRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<RealWorkout> _repository;

    public RealWorkoutRepository(AppDbContext dbContext, IRepository<RealWorkout> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<List<RealWorkout>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.RealWorkouts.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetAllAsync(Expression<Func<RealWorkout, bool>> wherePredicate, 
        bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.RealWorkouts.AsQueryable().Where(wherePredicate);
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetByDayAsync(DateOnly day, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.RealWorkouts
            .Where(x => x.Date == day)
            .OrderBy(x => x.StartTime)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, 
        DateOnly endDate, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.RealWorkouts
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }

    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, DateOnly endDate, 
        bool asNoTracking, CancellationToken token, params Expression<Func<RealWorkout, object>>[] includes)
    {
        var query = _dbContext.RealWorkouts
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query); 
        
        return await query.ToListAsync(token);
    }
    
    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.RealWorkouts.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token, 
        params Expression<Func<RealWorkout, object>>[] includes)
    {
         var query = _dbContext.RealWorkouts.AsQueryable();
         query = _repository.ApplyAsNoTracking(asNoTracking, query);
         query = _repository.ApplyIncludes(includes, query); 
         
         return await query.FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> AddAsync(RealWorkout realWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(realWorkout, token);
        await _dbContext.SaveChangesAsync(token);
        return realWorkout;
    }
    
    public async Task AddAsync(List<RealWorkout> realWorkouts, CancellationToken token)
    {
        await _dbContext.AddRangeAsync(realWorkouts, token);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task DeleteAsync(RealWorkout realWorkout, CancellationToken token)
    {
        _dbContext.Remove(realWorkout);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(RealWorkout realWorkout, CancellationToken token)
    {
        _dbContext.Update(realWorkout);
        await _dbContext.SaveChangesAsync(token);
    }
}
