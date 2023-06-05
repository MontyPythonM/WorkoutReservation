using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.TypesExtensions;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly AppDbContext _dbContext;

    public ApplicationUserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ApplicationUser> GetByEmailAsync(string email, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes)
    {
        return await _dbContext.ApplicationUsers
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(user => user.Email == email, token);
    }
    
    public async Task<ApplicationUser> GetByGuidAsync(Guid userId, bool asNoTracking = false, 
        CancellationToken token = default)
    { 
        return await _dbContext.ApplicationUsers
            .ApplyAsNoTracking(asNoTracking)
            .Include(user => user.ApplicationRoles)
            .ThenInclude(role => role.ApplicationPermissions)
            .FirstOrDefaultAsync(user => user.Id == userId, token);
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

    public async Task<(List<ApplicationUser> users, int totalItems)> GetPagedAsync(IPagedQuery request, 
        CancellationToken token)
    {
        var usersQuery = _dbContext.ApplicationUsers
            .AsNoTracking()
            .Include(x => x.ApplicationRoles);
        
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

    public async Task<List<ApplicationUser>> GetByRoleAsync(ApplicationRole role, bool asNoTracking = false, 
        CancellationToken token = default)
    {
       return await _dbContext.ApplicationUsers
           .Where(user => user.ApplicationRoles.Contains(role))
           .ApplyAsNoTracking(asNoTracking)
           .ToListAsync(token);
    }
}
