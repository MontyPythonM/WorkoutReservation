using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
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
                act.MapFrom(src => src.WorkoutTypeTags.Select(x => x.Id)));
        CreateMap<Instructor, InstructorForWorkoutTypeDto>();

        // Get
        CreateMap<WorkoutType, WorkoutTypeDetailQueryDto>();
        CreateMap<Instructor, InstructorForWorkoutTypeDetailDto>();

        // Create
        CreateMap<CreateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, act => act.Ignore());

        // Update
        CreateMap<UpdateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, act => act.Ignore());
    }
}
