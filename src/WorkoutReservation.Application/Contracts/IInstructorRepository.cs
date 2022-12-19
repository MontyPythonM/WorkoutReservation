using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IInstructorRepository
{
    public Task<List<Instructor>> GetAllAsync(bool asNoTracking, CancellationToken token);
    public Task<List<Instructor>> GetAllAsync(Expression<Func<Instructor, bool>> wherePredicate, bool asNoTracking, CancellationToken token);
    public Task<Instructor> GetByIdAsync(int instructorId, bool asNoTracking, CancellationToken token);
    public Task<Instructor> GetByIdAsync(int instructorId, Expression<Func<Instructor, object>>[] includes, bool asNoTracking, CancellationToken token);
    public Task<Instructor> AddAsync(Instructor instructor, CancellationToken token);
    public Task DeleteAsync(Instructor instructor, CancellationToken token);
    public Task UpdateAsync(Instructor instructor, CancellationToken token);
}