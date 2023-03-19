using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRepetitiveWorkoutRepository
{
    public Task AddAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task DeleteAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task DeleteAsync(IEnumerable<RepetitiveWorkout> repetitiveWorkouts, CancellationToken token);
    public Task UpdateAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task<List<RepetitiveWorkout>> GetAllAsync(bool asNoTracking,
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes);
    public Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek, bool asNoTracking,
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes);
    public Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId, bool asNoTracking,
        CancellationToken token, params Expression<Func<RepetitiveWorkout, object>>[] includes);

}
