using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

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

        var pagedWorkoutTypes = await _workoutTypeRepository.GetAllPagedAsync(request);

        return pagedWorkoutTypes;
    }
}
