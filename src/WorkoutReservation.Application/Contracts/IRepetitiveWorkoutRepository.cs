using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRepetitiveWorkoutRepository
{
    public Task AddAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task DeleteAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task UpdateAsync(RepetitiveWorkout repetitiveWorkout, CancellationToken token);
    public Task<List<RepetitiveWorkout>> GetAllAsync(CancellationToken token);
    public Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek, CancellationToken token);
    public Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId, CancellationToken token);
    public Task DeleteAsync(IEnumerable<RepetitiveWorkout> repetitiveWorkouts, CancellationToken token);
}
