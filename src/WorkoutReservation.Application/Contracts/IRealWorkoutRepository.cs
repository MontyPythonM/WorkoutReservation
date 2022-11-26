using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IRealWorkoutRepository
{
    public Task<List<RealWorkout>> GetAllAsync(DateOnly startDate, DateOnly endDate, CancellationToken token);
    public Task<RealWorkout> GetByIdWithReservationDetailsAsync(int realWorkoutId, CancellationToken token);
    public Task<RealWorkout> GetByIdAsync(int realWorkoutId, CancellationToken token);
    public Task<RealWorkout> AddAsync(RealWorkout realWorkout, CancellationToken token);
    public Task DeleteAsync(RealWorkout realWorkout, CancellationToken token);
    public Task UpdateAsync(RealWorkout realWorkout, CancellationToken token);
    public Task IncrementCurrentParticipantNumberAsync(RealWorkout realWorkout, CancellationToken token);
    public Task DecrementCurrentParticipantNumberAsync(RealWorkout realWorkout, CancellationToken token);
    public Task AddRangeAsync(List<RealWorkout> realWorkouts, CancellationToken token);
}
