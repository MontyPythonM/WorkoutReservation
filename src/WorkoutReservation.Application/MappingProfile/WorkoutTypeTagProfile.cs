using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

public class WorkoutTypeTagProfile : Profile
{
    public WorkoutTypeTagProfile()
    {
        CreateMap<WorkoutTypeTag, WorkoutTypeTagsDto>()
            .ForMember(desc => desc.NumberOfUses, opt => opt.MapFrom(src => src.WorkoutTypes.Count));
        
        CreateMap<WorkoutTypeTag, ActiveWorkoutTypeTagsDto>();
    }
}