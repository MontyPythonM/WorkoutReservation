﻿using AutoMapper;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetAllWorkoutTypes;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

public class WorkoutTypeProfile : Profile
{
    public WorkoutTypeProfile()
    {
        CreateMap<WorkoutType, WorkoutTypesListQueryDto>()
            .ForMember(dest => dest.WorkoutTypeTags, opt =>
                opt.MapFrom(src => src.WorkoutTypeTags.Select(x => x.Id)))
            .ForMember(dest => dest.Instructors, opt =>
                opt.MapFrom(src => src.Instructors.Select(x => x.Id)));

        CreateMap<WorkoutType, WorkoutTypesDto>();
    }
}
