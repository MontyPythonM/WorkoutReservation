using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.TypesExtensions;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<Reservation> _repository;

    public ReservationRepository(AppDbContext dbContext, IRepository<Reservation> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token, params Expression<Func<Reservation, object>>[] includes)
    {
        var query = _dbContext.Reservations.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == reservationId, token);
    }
    
    public async Task<Reservation> GetDetailsByIdAsync(int reservationId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Reservations
            .Include(r => r.RealWorkout).ThenInclude(w => w.Instructor)
            .Include(r => r.RealWorkout).ThenInclude(w => w.WorkoutType)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(r => r.Id == reservationId, token);
    }
    
    public async Task<(List<Reservation> reservations, int totalItems)> GetPagedAsync(IPagedQuery request, 
        Guid userId, CancellationToken token)
    {
        var reservationsQuery = _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.RealWorkout).ThenInclude(x => x.Instructor)
            .Include(x => x.RealWorkout).ThenInclude(x => x.WorkoutType)
            .Where(x => x.UserId == userId)
            .AsQueryable(); 
        
        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                        x.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { SortBySelector.ReservationStatus.StringValue(), u => u.ReservationStatus},
                { SortBySelector.WorkoutDate.StringValue(), u => u.RealWorkout.Date}
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

    public async Task<(List<Reservation> reservations, int totalItems)> GetPagedAsync(IPagedQuery request, 
        CancellationToken token)
    {
        var reservationsQuery = _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.RealWorkout).ThenInclude(x => x.Instructor)
            .Include(x => x.RealWorkout).ThenInclude(x => x.WorkoutType)
            .AsQueryable();    
        
        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                        x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));
        
        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { SortBySelector.ReservationId.StringValue(), u => u.Id },
                { SortBySelector.ReservationStatus.StringValue(), u => u.ReservationStatus },
                { SortBySelector.CreatedDate.StringValue(), u => u.CreatedDate },
                { SortBySelector.LastModifiedDate.StringValue(), u => u.LastModifiedDate },
                { SortBySelector.WorkoutDate.StringValue(), u => u.RealWorkout.Date },
                { SortBySelector.WorkoutName.StringValue(), u => u.RealWorkout.WorkoutType.Name }
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
}
