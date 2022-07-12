using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IWorkoutTypeRepository
{
    public Task<WorkoutType> AddAsync(WorkoutType workoutType);
    public Task DeleteAsync(WorkoutType workoutType);
    public Task UpdateAsync(WorkoutType workoutType);
    public Task<List<WorkoutType>> GetAllAsync();
    public Task<WorkoutType> GetByIdAsync(int workoutTypeId);
    public IQueryable<WorkoutType> GetAllQueriesAsync();
}
