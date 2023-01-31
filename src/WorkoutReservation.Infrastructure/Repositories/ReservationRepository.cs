using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
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

    public async Task<List<Reservation>> GetUserReservationsByGuidAsync(Guid userId, CancellationToken token)
    {
        return await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout)
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.RealWorkout.Date)
                .ThenBy(x => x.RealWorkout.StartTime)
            .ToListAsync(token);
    }

    public async Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken token)
    {
        await _dbContext.AddAsync(reservation, token);
        await _dbContext.SaveChangesAsync(token);

        return reservation;
    }

    public async Task<bool> CheckIsUserReservedAsync(RealWorkout realWorkout, ApplicationUser currentUser, CancellationToken token)
    {
        var isUserAlreadyReserved = await _dbContext.Reservations
            .Include(x => x.User)
            .Include(x => x.RealWorkout)
            .Where(x => x.RealWorkoutId == realWorkout.Id)
            .FirstOrDefaultAsync(x => x.UserId == currentUser.Id, token);
      
        return isUserAlreadyReserved is not null;
    }

    public async Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.Reservations.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
            
        return await query.FirstOrDefaultAsync(x => x.Id == reservationId, token);
    }
    
    public async Task<Reservation> GetByIdAsync(int reservationId, bool asNoTracking, CancellationToken token, params Expression<Func<Reservation, object>>[] includes)
    {
        var query = _dbContext.Reservations.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == reservationId, token);
    }
    
    public async Task<Reservation> GetReservationByIdAsync(int reservationId, CancellationToken token)
    {
        return await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == reservationId, token);
    }

    public async Task UpdateAsync(Reservation reservation, CancellationToken token)
    {
        _dbContext.Update(reservation);
        await _dbContext.SaveChangesAsync(token);
    }

    public IQueryable<Reservation> GetUserReservationsByGuidQuery(Guid userId)
    { 
        return _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout)
                .ThenInclude(x => x.Instructor)
            .Include(x => x.RealWorkout)
                .ThenInclude(x => x.WorkoutType)
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.RealWorkout.Date)
                .ThenBy(x => x.RealWorkout.StartTime)
            .AsQueryable();          
    }
}
