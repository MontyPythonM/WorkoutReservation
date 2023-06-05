using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _dbContext;

    public ContentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Content> GetLastContentAsync(ContentType type, bool asNoTracking = false, 
        CancellationToken token = default)
    {
        return await _dbContext.Contents
            .ApplyAsNoTracking(asNoTracking)
            .OrderByDescending(content => content.CreatedDate)
            .FirstOrDefaultAsync(content => content.Type == type);
    }

    public async Task CreateAsync(Content content, CancellationToken token)
    {
        await _dbContext.AddAsync(content, token);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task UpdateAsync(Content content, CancellationToken token)
    {
        _dbContext.Update(content);
        await _dbContext.SaveChangesAsync(token);
    }
}