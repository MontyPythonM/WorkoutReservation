using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IInstructorRepository
{
    public Task<Instructor> AddAsync(Instructor instructor, CancellationToken token);
    public Task DeleteAsync(Instructor instructor, CancellationToken token);
    public Task UpdateAsync(Instructor instructor, CancellationToken token);
    public Task<List<Instructor>> GetAllAsync(CancellationToken token);
    public Task<Instructor> GetByIdAsync(int instructorId, CancellationToken token);
}
