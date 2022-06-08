using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts
{
    public interface IInstructorRepository
    {
        public Task<Instructor> AddAsync(Instructor instructor);
        public Task DeleteAsync(Instructor instructor);
        public Task UpdateAsync(Instructor instructor);
        public Task<List<Instructor>> GetAllAsync();
        public Task<Instructor> GetByIdAsync(int instructorId);
    }
}
