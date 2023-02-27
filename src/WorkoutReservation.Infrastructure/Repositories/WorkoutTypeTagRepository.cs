using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeTagRepository : IWorkoutTypeTagRepository
{    
    private readonly AppDbContext _dbContext;
    private readonly IRepository<WorkoutTypeTag> _repository;

    public WorkoutTypeTagRepository(AppDbContext dbContext, IRepository<WorkoutTypeTag> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(bool asNoTracking, CancellationToken token, params Expression<Func<WorkoutTypeTag, object>>[] includes)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.ToListAsync(token);
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(Expression<Func<WorkoutTypeTag, bool>> wherePredicate, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable().Where(wherePredicate);
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }

    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }
    
    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<WorkoutTypeTag, object>>[] includes)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }

    public async Task AddAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutTypeTag, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        _dbContext.Remove(workoutTypeTag);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task UpdateAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        _dbContext.Update(workoutTypeTag);
        await _dbContext.SaveChangesAsync(token);
    }
}