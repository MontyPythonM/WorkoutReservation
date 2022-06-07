using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutType.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutType.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile
{
    public class WorkoutTypeProfile : Profile
    {
        public WorkoutTypeProfile()
        {
            CreateMap<WorkoutType, WorkoutTypeDetailDto>();

            CreateMap<WorkoutType, WorkoutTypesListDto>();

            CreateMap<Instructor, InstructorDto>();

            CreateMap<WorkoutTypeTag, WorkoutTypeTagDto>();
        }
    }
}
