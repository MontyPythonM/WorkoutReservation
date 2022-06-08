using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories
{
    public class WorkoutTypeRepository : IWorkoutTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public WorkoutTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkoutType> AddAsync(WorkoutType workoutType)
        {
            await _dbContext.AddAsync(workoutType);
            await _dbContext.SaveChangesAsync();

            return workoutType;
        }

        public async Task DeleteAsync(WorkoutType workoutType)
        {
            _dbContext.Remove(workoutType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutType workoutType)
        {
            _dbContext.Update(workoutType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<WorkoutType>> GetAllAsync()
        {
            return await _dbContext.WorkoutTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkoutType> GetByIdAsync(int workoutTypeId)
        {
            return await _dbContext.WorkoutTypes
                .AsNoTracking()
                .Include(x => x.Instructors)
                .Include(x => x.WorkoutTypeTags)
                .FirstOrDefaultAsync(x => x.Id == workoutTypeId);
        }

    }
}
