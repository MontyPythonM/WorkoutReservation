using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeRepository : IWorkoutTypeRepository
{
    private readonly AppDbContext _dbContext;

    public WorkoutTypeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkoutType> AddAsync(WorkoutType workoutType, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutType, token);
        await _dbContext.SaveChangesAsync(token);

        return workoutType;
    }

    public async Task DeleteAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Remove(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Update(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<List<WorkoutType>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.WorkoutTypes
            .AsNoTracking()
            .ToListAsync(token);
    }

    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, CancellationToken token)
    {
        return await _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(x => x.Instructors)
            .Include(x => x.WorkoutTypeTags)
            .Include(x => x.BaseWorkouts)
            .FirstOrDefaultAsync(x => x.Id == workoutTypeId, token);
    }

    public IQueryable<WorkoutType> GetAllQuery()
    {
        return _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(x => x.Instructors)
            .Include(x => x.WorkoutTypeTags)
            .AsQueryable();
    }
}
