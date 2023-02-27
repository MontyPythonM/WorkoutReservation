using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

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
    
    public async Task AddReservationAsync(Reservation reservation, CancellationToken token)
    {
        await _dbContext.AddAsync(reservation, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<bool> CheckIsReservedAsync(RealWorkout realWorkout, ApplicationUser user, CancellationToken token)
    {
        var isUserAlreadyReserved = await _dbContext.Reservations
            .Include(x => x.User)
            .Include(x => x.RealWorkout)
            .Where(x => x.RealWorkoutId == realWorkout.Id && x.ReservationStatus != ReservationStatus.Cancelled)
            .FirstOrDefaultAsync(x => x.UserId == user.Id, token);

        return isUserAlreadyReserved is not null;
    }

    public async Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token, params Expression<Func<Reservation, object>>[] includes)
    {
        var query = _dbContext.Reservations.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == reservationId, token);
    }

    public async Task UpdateAsync(Reservation reservation, CancellationToken token)
    {
        _dbContext.Update(reservation);
        await _dbContext.SaveChangesAsync(token);
    }

    public IQueryable<Reservation> GetUserReservationsQuery(Guid userId)
    { 
        return _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout).ThenInclude(x => x.Instructor)
            .Include(x => x.RealWorkout).ThenInclude(x => x.WorkoutType)
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.RealWorkout.Date).ThenBy(x => x.RealWorkout.StartTime)
            .AsQueryable();          
    }
    
    public async Task<Reservation> GetDetailsByIdAsync(int reservationId, Guid userId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Reservations
            .Include(r => r.RealWorkout).ThenInclude(w => w.Instructor)
            .Include(r => r.RealWorkout).ThenInclude(w => w.WorkoutType)
            .AsQueryable();
        
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        
        return await query.FirstOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId, token);
    }
}
