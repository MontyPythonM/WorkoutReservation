using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Shared.TypesExtensions;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _dbContext;

    public ReservationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<Reservation, object>>[] includes)
    {
        return await _dbContext.Reservations
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(reservation => reservation.Id == reservationId, token);
    }
    
    public async Task<Reservation> GetDetailsByIdAsync(int reservationId, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.Reservations
            .ApplyAsNoTracking(asNoTracking)
            .Include(reservation => reservation.RealWorkout)
            .ThenInclude(realWorkout => realWorkout.Instructor)
            .Include(reservation => reservation.RealWorkout)
            .ThenInclude(realWorkout => realWorkout.WorkoutType)
            .FirstOrDefaultAsync(reservation => reservation.Id == reservationId, token);
    }
    
    public async Task<(List<Reservation> reservations, int totalItems)> GetPagedAsync(IPagedQuery request, 
        Guid userId, CancellationToken token)
    {
        var reservationsQuery = _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.RealWorkout).ThenInclude(r => r.Instructor)
            .Include(r => r.RealWorkout).ThenInclude(r => r.WorkoutType)
            .Where(r => r.UserId == userId);
        
        var query = reservationsQuery
            .Where(r => request.SearchPhrase == null ||
                        r.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.User.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.User.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.User.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()));

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
            .Include(r => r.User)
            .Include(r => r.RealWorkout).ThenInclude(r => r.Instructor)
            .Include(r => r.RealWorkout).ThenInclude(r => r.WorkoutType);
        
        var query = reservationsQuery
            .Where(r => request.SearchPhrase == null ||
                        r.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        r.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));
        
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
