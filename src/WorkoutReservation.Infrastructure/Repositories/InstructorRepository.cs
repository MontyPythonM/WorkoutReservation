using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class InstructorRepository : IInstructorRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<Instructor> _repository;

    public InstructorRepository(AppDbContext dbContext, IRepository<Instructor> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
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
        var query = _dbContext.Instructors.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }

    public async Task<List<Instructor>> GetAllAsync(Expression<Func<Instructor, bool>> wherePredicate, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Instructors.AsQueryable().Where(wherePredicate);
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.ToListAsync(token);
    }

    public async Task<Instructor> GetByIdAsync(int instructorId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Instructors.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == instructorId, token);
    }
    
    public async Task<Instructor> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<Instructor, object>>[] includes)
    {
        var query = _dbContext.Instructors.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);   
        
        return await query.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }
}
