using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRepetitiveWorkoutRepository
{
    public Task<RepetitiveWorkout> AddAsync(RepetitiveWorkout repetitiveWorkout);
    public Task DeleteAsync(RepetitiveWorkout repetitiveWorkout);
    public Task UpdateAsync(RepetitiveWorkout repetitiveWorkout);
    public Task<List<RepetitiveWorkout>> GetAllAsync();
    public Task<List<RepetitiveWorkout>> GetAllFromSelectedDayAsync(DayOfWeek dayOfWeek);
    public Task<RepetitiveWorkout> GetByIdAsync(int repetitiveWorkoutId);
    public Task DeleteAllAsync(List<RepetitiveWorkout> repetitiveWorkouts);
}
