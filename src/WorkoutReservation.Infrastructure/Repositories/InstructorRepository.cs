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

    public async Task<Instructor> AddAsync(Instructor instructor)
    {
        await _dbContext.AddAsync(instructor);
        await _dbContext.SaveChangesAsync();

        return instructor;
    }

    public async Task DeleteAsync(Instructor instructor)
    {
        _dbContext.Remove(instructor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Instructor instructor)
    { 
        _dbContext.Update(instructor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Instructor>> GetAllAsync()
    {
        return await _dbContext.Instructors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Instructor> GetByIdAsync(int instructorId)
    {
        return await _dbContext.Instructors
            .AsNoTracking()
            .Include(x => x.WorkoutTypes)
            .FirstOrDefaultAsync(x => x.Id == instructorId);
    }
}
