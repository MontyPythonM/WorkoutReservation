using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

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

    public async Task<RealWorkout> GetByIdWithReservationDetailsAsync(int realworkoutId)
    {
        return await _dbContext.RealWorkouts
            .AsNoTracking()
            .Include(x => x.Reservations)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == realworkoutId);
    }

    public async Task<RealWorkout> AddAsync(RealWorkout realWorkout)
    {
        await _dbContext.AddAsync(realWorkout);
        await _dbContext.SaveChangesAsync();

        return realWorkout;
    }

    public async Task DeleteAsync(RealWorkout realWorkout)
    {
        _dbContext.Remove(realWorkout);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RealWorkout realWorkout)
    {
        _dbContext.Update(realWorkout);
        await _dbContext.SaveChangesAsync();
    }

    public async Task IncrementCurrentParticipianNumber(RealWorkout realWorkout)
    {
        var result = await _dbContext.RealWorkouts
            .FirstOrDefaultAsync(x => x.Id == realWorkout.Id);

        result.CurrentParticipiantNumber++; 
        await _dbContext.SaveChangesAsync();
    }
    public async Task DecrementCurrentParticipianNumber(RealWorkout realWorkout)
    {
        var result = await _dbContext.RealWorkouts
            .FirstOrDefaultAsync(x => x.Id == realWorkout.Id);

        result.CurrentParticipiantNumber--;
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<RealWorkout> realWorkouts)
    {
        await _dbContext.AddRangeAsync(realWorkouts);
        await _dbContext.SaveChangesAsync();
    }

}
