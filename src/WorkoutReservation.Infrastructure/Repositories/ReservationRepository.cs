using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _dbContext;

    public ReservationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Reservation>> GetUserReservationsByGuidAsync(Guid userId)
    {
        return await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout)
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.RealWorkout.Date)
                .ThenBy(x => x.RealWorkout.StartTime)
            .ToListAsync();
    }

    public async Task<Reservation> AddReservationAsync(Reservation reservation)
    {
        await _dbContext.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task<bool> CheckUserReservationAsync(int workoutId, Guid currentUserId)
    {
        var isUserAlreadyReserved = await _dbContext.Reservations
            .Include(x => x.User)
            .Include(x => x.RealWorkout)
            .Where(x => x.RealWorkoutId == workoutId)
            .FirstOrDefaultAsync(x => x.UserId == currentUserId);
      
        return isUserAlreadyReserved is not null;
    }

    public async Task<Reservation> GetReservationByIdAsync(int reservationId)
    {
        return await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.RealWorkout)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == reservationId);
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
        _dbContext.Update(reservation);
        await _dbContext.SaveChangesAsync();
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
