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
        CreateMap<WorkoutType, WorkoutTypesListQueryDto>()
            .ForMember(dest => dest.WorkoutTypeTags, opt =>
                opt.MapFrom(src => src.WorkoutTypeTags.Select(x => x.Id)))
            .ForMember(dest => dest.Instructors, opt =>
                opt.MapFrom(src => src.Instructors.Select(x => x.Id)));

        CreateMap<CreateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, opt => opt.Ignore())
            .ForMember(dest => dest.Instructors, opt => opt.Ignore());

        CreateMap<UpdateWorkoutTypeCommand, WorkoutType>()
            .ForMember(dest => dest.WorkoutTypeTags, opt => opt.Ignore())
            .ForMember(dest => dest.Instructors, opt => opt.Ignore());
    }
}
