using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Dtos;

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

        var pagedResult = await _workoutTypeRepository.GetPagedAsync(request, token);
        var mappedResult = _mapper.Map<List<WorkoutTypesListQueryDto>>(pagedResult.workoutTypes);

        return new PagedResultDto<WorkoutTypesListQueryDto>(mappedResult, 
            pagedResult.totalItems, request.PageSize, request.PageNumber);
    }
}
