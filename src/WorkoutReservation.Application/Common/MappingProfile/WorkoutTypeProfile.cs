using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
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

            CreateMap<CreateWorkoutTypeCommand, WorkoutType>();

            CreateMap<UpdateWorkoutTypeCommand, WorkoutType>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.WorkoutTypeId));
        }
    }
}
