using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Application.Contracts
{
    public interface IRealWorkoutRepository
    {
        public Task<List<RealWorkout>> GetAllAsync(DateOnly startDate, DateOnly endDate);
    }
}
