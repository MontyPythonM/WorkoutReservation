using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

public class GetWorkoutTypesListQueryHandler : IRequestHandler<GetWorkoutTypesListQuery,
                                                               PagedResultDto<WorkoutTypesListQueryDto>>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;

    public GetWorkoutTypesListQueryHandler(IWorkoutTypeRepository workoutTypeRepository)
    {
        _workoutTypeRepository = workoutTypeRepository;
    }

    public async Task<PagedResultDto<WorkoutTypesListQueryDto>> Handle(GetWorkoutTypesListQuery request, 
                                                                       CancellationToken cancellationToken)
    {
        var validator = new GetWorkoutTypesListQueryValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var workoutTypesQuery = _workoutTypeRepository.GetAllQueriesAsync();

        var query = workoutTypesQuery
            .Where(x => request.SearchPhrase == null ||
                   x.Name.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<WorkoutType, object>>>
            {
                { nameof(WorkoutType.Name), u => u.Name},
                { nameof(WorkoutType.Intensity), u => u.Intensity},
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        var result = query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .Select(x => new WorkoutTypesListQueryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Intensity = x.Intensity
                })
                .ToList();

        var pagedWorkoutTypes = new PagedResultDto<WorkoutTypesListQueryDto>(result,
                                                                             totalCount,
                                                                             request.PageSize,
                                                                             request.PageNumber);
        return pagedWorkoutTypes;
    }
}
