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

    public async Task<List<Instructor>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Instructors.AsQueryable();
        if (asNoTracking)
            query.AsNoTracking();
        
        return await query.ToListAsync(token);
    }

    public async Task<List<Instructor>> GetAllAsync(Expression<Func<Instructor, bool>> wherePredicate, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Instructors.AsQueryable().Where(wherePredicate);
        if (asNoTracking)
            query.AsNoTracking();
        
        return await query.ToListAsync(token);
    }

    public async Task<Instructor> GetByIdAsync(int instructorId, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.Instructors.Include(x => x.WorkoutTypes);
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == instructorId, token);
    }
    
    public async Task<Instructor> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<Instructor, object>>[] includes)
    {
        var baseQuery = _dbContext.Instructors.AsQueryable();
                
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                baseQuery = baseQuery.Include(include);
            }
        }
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }
}
