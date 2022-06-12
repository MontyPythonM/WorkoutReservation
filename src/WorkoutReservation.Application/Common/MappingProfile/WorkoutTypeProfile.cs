using AutoMapper;
using WorkoutReservation.Application.Common.Models;
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
            // WorkoutTypesListQueryDto
            CreateMap<WorkoutType, WorkoutTypesListQueryDto>();

            // WorkoutTypeDetailQueryDto
            CreateMap<WorkoutType, WorkoutTypeDetailQueryDto>();
            CreateMap<WorkoutTypeTag, WorkoutTypeTagDto>();

            // CreateWorkoutTypeCommand
            CreateMap<CreateWorkoutTypeCommand, WorkoutType>();

            // UpdateWorkoutTypeCommand
            CreateMap<UpdateWorkoutTypeCommand, WorkoutType>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.WorkoutTypeId));

            // Common dtos
            CreateMap<WorkoutType, WorkoutTypeDto>();
        }
    }
}
