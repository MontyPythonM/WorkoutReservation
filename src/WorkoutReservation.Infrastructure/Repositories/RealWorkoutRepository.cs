using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class RealWorkoutRepository : IRealWorkoutRepository
{
    private readonly AppDbContext _dbContext;

    public RealWorkoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<RealWorkout>> GetByDayAsync(DateOnly day, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .Where(x => x.Date == day)
            .OrderBy(x => x.StartTime)
            .ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, 
        DateOnly endDate, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, DateOnly endDate, 
        bool asNoTracking, CancellationToken token, params Expression<Func<RealWorkout, object>>[] includes)
    {
        return await _dbContext.RealWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .ToListAsync(token);
    }
    
    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token, 
        params Expression<Func<RealWorkout, object>>[] includes)
    {
         return await _dbContext.RealWorkouts
             .ApplyAsNoTracking(asNoTracking)
             .ApplyIncludes(includes)
             .FirstOrDefaultAsync(realWorkout => realWorkout.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> GetByIdWithReservationUserAsync(int realWorkoutId, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.WorkoutType)
            .Include(r => r.Instructor)
            .Include(r => r.Reservations)
            .ThenInclude(reservation => reservation.User)
            .FirstOrDefaultAsync(realWorkout => realWorkout.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> GetByReservationIdAsync(int reservationId, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.WorkoutType)
            .Include(r => r.Instructor)
            .Include(r => r.Reservations)
            .ThenInclude(reservation => reservation.User)
            .Where(r => r.Reservations.Any(reservation => reservation.Id == reservationId))
            .FirstOrDefaultAsync(token);
    }
    
    public async Task AddAsync(RealWorkout realWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(realWorkout, token);
        await _dbContext.SaveChangesAsync(token);
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
