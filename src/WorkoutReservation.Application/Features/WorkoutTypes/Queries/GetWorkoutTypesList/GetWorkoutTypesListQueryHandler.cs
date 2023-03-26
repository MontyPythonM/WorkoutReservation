using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

public record GetWorkoutTypesListQuery : IRequest<PagedResultDto<WorkoutTypesListQueryDto>>, IPagedQuery
{
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal sealed class GetWorkoutTypesListQueryHandler : IRequestHandler<GetWorkoutTypesListQuery,
    PagedResultDto<WorkoutTypesListQueryDto>>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IMapper _mapper;

    public GetWorkoutTypesListQueryHandler(IWorkoutTypeRepository workoutTypeRepository, IMapper mapper)
    {
        _workoutTypeRepository = workoutTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResultDto<WorkoutTypesListQueryDto>> Handle(GetWorkoutTypesListQuery request, 
        CancellationToken token)
    {
        var validator = new GetWorkoutTypesListQueryValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var workoutTypesQuery = _workoutTypeRepository.GetAllQuery();

        var query = workoutTypesQuery
            .Where(x => request.SearchPhrase == null ||
                   x.Name.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<WorkoutType, object>>>
            {
                { SortBySelector.WorkoutName.StringValue(), u => u.Name},
                { SortBySelector.WorkoutIntensity.StringValue(), u => u.Intensity},
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        var result = query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToList();

        var mappedResult = _mapper.Map<List<WorkoutTypesListQueryDto>>(result);

        return new PagedResultDto<WorkoutTypesListQueryDto>(mappedResult, 
            totalCount, request.PageSize, request.PageNumber);
    }
}
