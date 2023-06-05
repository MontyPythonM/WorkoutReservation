using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.TypesExtensions;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeRepository : IWorkoutTypeRepository
{
    private readonly AppDbContext _dbContext;

    public WorkoutTypeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<WorkoutType>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.WorkoutTypes
            .ApplyAsNoTracking(asNoTracking)
            .ToListAsync(token);
    }

    public async Task<(List<WorkoutType> workoutTypes, int totalItems)> GetPagedAsync(IPagedQuery request,
        CancellationToken token)
    {
        var workoutTypesQuery = _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(w => w.Instructors)
            .Include(w => w.WorkoutTypeTags);
        
        var query = workoutTypesQuery
            .Where(w => request.SearchPhrase == null ||
                        w.Name.ToLower().Contains(request.SearchPhrase.ToLower()));

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
    
    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<WorkoutType, object>>[] includes)
    {
        return await _dbContext.WorkoutTypes
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(workoutType => workoutType.Id == workoutTypeId, token);
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

    public async Task UpdateAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Update(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }
}
