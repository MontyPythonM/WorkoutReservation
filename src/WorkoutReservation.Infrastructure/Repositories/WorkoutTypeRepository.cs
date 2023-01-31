using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeRepository : IWorkoutTypeRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<WorkoutType> _repository;

    public WorkoutTypeRepository(AppDbContext dbContext, IRepository<WorkoutType> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<List<WorkoutType>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypes.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }
    
    public IQueryable<WorkoutType> GetAllQuery()
    {
        return _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(x => x.Instructors)
            .Include(x => x.WorkoutTypeTags)
            .AsQueryable();
    }
    
    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypes.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == workoutTypeId, token);
    }
    
    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking = false, CancellationToken token = default, params Expression<Func<WorkoutType, object>>[] includes)
    {
        var query = _dbContext.WorkoutTypes.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == workoutTypeId, token);
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

    public async Task DeleteAsync(List<WorkoutType> workoutTypes, CancellationToken token)
    {
        _dbContext.RemoveRange(workoutTypes);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Update(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }
}
