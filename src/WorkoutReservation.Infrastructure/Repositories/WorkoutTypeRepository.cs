using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
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

        public async Task<PagedResultDto<WorkoutTypesListQueryDto>> GetAllPagedAsync(GetWorkoutTypesListQuery clientRequest)
        {
            var workoutTypes = _dbContext.WorkoutTypes.AsQueryable();

            var query = workoutTypes
                .Where(x => clientRequest.SearchPhrase == null ||
                       x.Name.ToLower().Contains(clientRequest.SearchPhrase.ToLower()));

            var totalCount = query.Count();

            if (!string.IsNullOrEmpty(clientRequest.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<WorkoutType, object>>>
                {
                    { nameof(WorkoutType.Name), u => u.Name},
                    { nameof(WorkoutType.Intensity), u => u.Intensity},
                };

                var sortByExpression = columnsSelector[clientRequest.SortBy];

                query = clientRequest.SortByDescending
                    ? query.OrderByDescending(sortByExpression)
                    : query.OrderBy(sortByExpression);
            }

            var result = await query
                    .AsNoTracking()
                    .Skip(clientRequest.PageSize * (clientRequest.PageNumber - 1))
                    .Take(clientRequest.PageSize)
                    .Select(x => new WorkoutTypesListQueryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Intensity = x.Intensity
                    })
                    .ToListAsync();

            var pagedResult = new PagedResultDto<WorkoutTypesListQueryDto>(result, 
                                                                           totalCount,
                                                                           clientRequest.PageSize,
                                                                           clientRequest.PageNumber);
            return pagedResult;
        }
    }
}
