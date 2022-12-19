using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class WorkoutTypeProfile : Profile
{
    public WorkoutTypeProfile()
    {
        // Get list
        CreateMap<WorkoutType, WorkoutTypesListQueryDto>()
            .ForMember(dest => dest.WorkoutTypeTags, act =>
                act.MapFrom(src => src.WorkoutTypeTags.Select(x => x.Id)))
            .ForMember(dest => dest.Instructors, act =>
                act.MapFrom(src => src.Instructors.Select(x => x.Id)));

        // Create
        CreateMap<CreateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, act => act.Ignore())
            .ForMember(dest => dest.Instructors, act => act.Ignore());

        // Update
        CreateMap<UpdateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, act => act.Ignore())
            .ForMember(dest => dest.Instructors, act => act.Ignore());
    }
}
