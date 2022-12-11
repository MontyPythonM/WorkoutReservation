using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTagsList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class WorkoutTypeTagProfile : Profile
{
    public WorkoutTypeTagProfile()
    {
        // GetWorkoutTypeTagsList
        CreateMap<WorkoutTypeTag, WorkoutTypeTagsListDto>();

        // CreateWorkoutTypeTag
        CreateMap<CreateWorkoutTypeTagCommand, WorkoutTypeTag>();
    }
}