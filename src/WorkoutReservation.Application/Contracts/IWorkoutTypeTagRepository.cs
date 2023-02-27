using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IWorkoutTypeTagRepository
{
    public Task<List<WorkoutTypeTag>> GetAllAsync(bool asNoTracking, CancellationToken token,
        params Expression<Func<WorkoutTypeTag, object>>[] includes);
    public Task<List<WorkoutTypeTag>> GetAllAsync(Expression<Func<WorkoutTypeTag, bool>> wherePredicate, 
        bool asNoTracking, CancellationToken token);
    public Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, CancellationToken token);
    public Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, CancellationToken token, 
        params Expression<Func<WorkoutTypeTag, object>>[] includes);
    public Task AddAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token);
    public Task DeleteAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token);
    public Task UpdateAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token);
}