using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRealWorkoutRepository
{
    public Task<List<RealWorkout>> GetAllAsync(bool asNoTracking, CancellationToken token);
    public Task<List<RealWorkout>> GetAllAsync(Expression<Func<RealWorkout, bool>> wherePredicate, 
        bool asNoTracking, CancellationToken token);
    public Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate,
        DateOnly endDate, bool asNoTracking, CancellationToken token);
    public Task<List<RealWorkout>> GetByDayAsync(DateOnly day, bool asNoTracking, CancellationToken token);
    public Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, DateOnly endDate, 
        bool asNoTracking, CancellationToken token, params Expression<Func<RealWorkout, object>>[] includes);
    public Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token);
    public Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token, 
        params Expression<Func<RealWorkout, object>>[] includes);
    public Task<RealWorkout> AddAsync(RealWorkout realWorkout, CancellationToken token);
    public Task AddAsync(List<RealWorkout> realWorkouts, CancellationToken token);
    public Task DeleteAsync(RealWorkout realWorkout, CancellationToken token);
    public Task UpdateAsync(RealWorkout realWorkout, CancellationToken token);
}
