using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class InstructorRepository : IInstructorRepository
{
    private readonly AppDbContext _dbContext;

    public InstructorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Instructor instructor, CancellationToken token)
    {
        await _dbContext.AddAsync(instructor, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Instructor instructor, CancellationToken token)
    {
        _dbContext.Remove(instructor);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Instructor instructor, CancellationToken token)
    { 
        _dbContext.Update(instructor);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<List<Instructor>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.Instructors
            .ApplyAsNoTracking(asNoTracking)
            .ToListAsync(token);
    }

    public async Task<List<Instructor>> GetAllAsync(Expression<Func<Instructor, bool>> wherePredicate, bool asNoTracking, 
        CancellationToken token)
    {
        return await _dbContext.Instructors
            .Where(wherePredicate)
            .ApplyAsNoTracking(asNoTracking)
            .ToListAsync(token);
    }
    
    public async Task<Instructor> GetByIdAsync(int instructorId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<Instructor, object>>[] includes)
    {
        return await _dbContext.Instructors
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(instructor => instructor.Id == instructorId, token);
    }
}
