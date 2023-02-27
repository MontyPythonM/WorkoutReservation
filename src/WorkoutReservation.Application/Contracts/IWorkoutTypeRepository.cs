using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IWorkoutTypeRepository
{
    public Task<List<WorkoutType>> GetAllAsync(bool asNoTracking, CancellationToken token);
    public IQueryable<WorkoutType> GetAllQuery();
    public Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking, CancellationToken token);
    public Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<WorkoutType, object>>[] includes);    
    public Task AddAsync(WorkoutType workoutType, CancellationToken token);
    public Task DeleteAsync(WorkoutType workoutType, CancellationToken token);
    public Task DeleteAsync(List<WorkoutType> workoutTypes, CancellationToken token);
    public Task UpdateAsync(WorkoutType workoutType, CancellationToken token);
}
