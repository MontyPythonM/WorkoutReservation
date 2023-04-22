using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.Extensions;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<ApplicationUser> _repository;

    public ApplicationUserRepository(AppDbContext dbContext, IRepository<ApplicationUser> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }
    
    public async Task<ApplicationUser> GetByEmailAsync(string email, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes)
    {
        var query = _dbContext.ApplicationUsers.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(user => user.Email == email, token);
    }
    
    public async Task<ApplicationUser> GetByGuidAsync(Guid guid, bool asNoTracking = false, 
        CancellationToken token = default)
    { 
        var query = _dbContext.ApplicationUsers
            .Include(user => user.ApplicationRoles)
            .ThenInclude(role => role.ApplicationPermissions)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == guid, token);
    }
    
    public async Task AddAsync(ApplicationUser user, CancellationToken token)
    { 
        await _dbContext.ApplicationUsers.AddAsync(user, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(ApplicationUser user, CancellationToken token)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<(List<ApplicationUser> users, int totalItems)> GetPagedAsync(IPagedQuery request, CancellationToken token)
    {
        var usersQuery = _dbContext.ApplicationUsers
            .AsNoTracking()
            .Include(x => x.ApplicationRoles)
            .AsQueryable();
        
        var query = usersQuery
            .Where(x => request.SearchPhrase == null ||
                        x.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.Email.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<ApplicationUser, object>>>
            {
                { SortBySelector.UserId.StringValue(), u => u.Id},
                { SortBySelector.UserEmail.StringValue(), u => u.Email},
                { SortBySelector.UserFirstName.StringValue(), u => u.FirstName},
                { SortBySelector.UserLastName.StringValue(), u => u.LastName},
                { SortBySelector.CreatedDate.StringValue(), u => u.CreatedDate}
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        return (await query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync(token), totalCount);
    }
    
    public async Task<bool> IsEmailAlreadyTaken(string email, CancellationToken token)
    {
        return await _dbContext.ApplicationUsers
            .FirstOrDefaultAsync(user => user.Email == email, token) is not null;
    }
}
