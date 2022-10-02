using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRealWorkoutRepository
{
    public Task<List<RealWorkout>> GetAllAsync(DateOnly startDate, DateOnly endDate);
    public Task<RealWorkout> GetByIdWithReservationDetailsAsync(int realWorkoutId);
    public Task<RealWorkout> GetByIdAsync(int realWorkoutId);
    public Task<RealWorkout> AddAsync(RealWorkout realWorkout);
    public Task DeleteAsync(RealWorkout realWorkout);
    public Task UpdateAsync(RealWorkout realWorkout);
    public Task IncrementCurrentParticipiantNumber(RealWorkout realWorkout);
    public Task DecrementCurrentParticipiantNumber(RealWorkout realWorkout);
    public Task AddRangeAsync(List<RealWorkout> realWorkouts);
}
