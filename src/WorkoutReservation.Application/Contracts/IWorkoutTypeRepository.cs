using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IWorkoutTypeRepository
{
    public Task<WorkoutType> AddAsync(WorkoutType workoutType, CancellationToken token);
    public Task DeleteAsync(WorkoutType workoutType, CancellationToken token);
    public Task UpdateAsync(WorkoutType workoutType, CancellationToken token);
    public Task<List<WorkoutType>> GetAllAsync(CancellationToken token);
    public Task<WorkoutType> GetByIdAsync(int workoutTypeId, CancellationToken token);
    public IQueryable<WorkoutType> GetAllQuery();
}
