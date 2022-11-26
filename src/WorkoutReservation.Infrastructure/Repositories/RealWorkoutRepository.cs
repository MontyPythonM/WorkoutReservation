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

    public async Task<List<RealWorkout>> GetAllAsync(DateOnly startDate, DateOnly endDate, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .AsNoTracking()
            .Include(x => x.Instructor)
            .Include(x => x.WorkoutType)
            .Where(x => x.Date >= startDate && x.Date < endDate)
            .OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
            .ToListAsync(token);
    }

    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .AsNoTracking()
            .Include(x => x.Instructor)
            .Include(x => x.WorkoutType)
            .FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);             
    }

    public async Task<RealWorkout> GetByIdWithReservationDetailsAsync(int realWorkoutId, CancellationToken token)
    {
        return await _dbContext.RealWorkouts
            .AsNoTracking()
            .Include(x => x.Reservations)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);
    }

    public async Task<RealWorkout> AddAsync(RealWorkout realWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(realWorkout, token);
        await _dbContext.SaveChangesAsync(token);

        return realWorkout;
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

    public async Task IncrementCurrentParticipantNumberAsync(RealWorkout realWorkout, CancellationToken token)
    {
        var result = await _dbContext.RealWorkouts
            .FirstOrDefaultAsync(x => x.Id == realWorkout.Id, token);

        result.CurrentParticipantNumber++; 
        await _dbContext.SaveChangesAsync(token);
    }
    public async Task DecrementCurrentParticipantNumberAsync(RealWorkout realWorkout, CancellationToken token)
    {
        var result = await _dbContext.RealWorkouts
            .FirstOrDefaultAsync(x => x.Id == realWorkout.Id, token);

        result.CurrentParticipantNumber--;
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task AddRangeAsync(List<RealWorkout> realWorkouts, CancellationToken token)
    {
        await _dbContext.AddRangeAsync(realWorkouts, token);
        await _dbContext.SaveChangesAsync(token);
    }

}
