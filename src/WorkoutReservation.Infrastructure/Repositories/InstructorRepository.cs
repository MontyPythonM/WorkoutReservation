using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class InstructorRepository : IInstructorRepository
{
    private readonly AppDbContext _dbContext;

    public InstructorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Instructor> AddAsync(Instructor instructor, CancellationToken token)
    {
        await _dbContext.AddAsync(instructor, token);
        await _dbContext.SaveChangesAsync(token);

        return instructor;
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

    public async Task<List<Instructor>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.Instructors
            .AsNoTracking()
            .ToListAsync(token);
    }

    public async Task<Instructor> GetByIdAsync(int instructorId, CancellationToken token)
    {
        return await _dbContext.Instructors
            .AsNoTracking()
            .Include(x => x.WorkoutTypes)
            .FirstOrDefaultAsync(x => x.Id == instructorId, token);
    }
}
