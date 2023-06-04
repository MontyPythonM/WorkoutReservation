using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<Content> _repository;

    public ContentRepository(AppDbContext dbContext, IRepository<Content> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }
    
    public async Task<Content> GetLastContentAsync(ContentType type, bool asNoTracking = false, CancellationToken token = default)
    {
        var query = _dbContext.Contents.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);

        return await query
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