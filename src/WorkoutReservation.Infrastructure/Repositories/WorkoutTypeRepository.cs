using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.Extensions;

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

    public async Task<(List<WorkoutType> workoutTypes, int totalItems)> GetPagedAsync(IPagedQuery request,
        CancellationToken token)
    {
        var workoutTypesQuery = _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(x => x.Instructors)
            .Include(x => x.WorkoutTypeTags)
            .AsQueryable();
        
        var query = workoutTypesQuery
            .Where(x => request.SearchPhrase == null ||
                        x.Name.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<WorkoutType, object>>>
            {
                { SortBySelector.WorkoutName.StringValue(), u => u.Name},
                { SortBySelector.WorkoutIntensity.StringValue(), u => u.Intensity},
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        return (await query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync(token), totalCount);
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
    
    public async Task AddAsync(WorkoutType workoutType, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutType, token);
        await _dbContext.SaveChangesAsync(token);
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
