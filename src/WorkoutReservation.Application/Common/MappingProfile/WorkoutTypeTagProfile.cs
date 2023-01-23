using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class WorkoutTypeTagProfile : Profile
{
    public WorkoutTypeTagProfile()
    {
        CreateMap<WorkoutTypeTag, WorkoutTypeTagsDto>()
            .ForMember(desc => desc.NumberOfUses, src => src.MapFrom(tag => tag.WorkoutTypes.Count));
        
        CreateMap<WorkoutTypeTag, ActiveWorkoutTypeTagsDto>();

        CreateMap<CreateWorkoutTypeTagCommand, WorkoutTypeTag>();
    }
}