﻿using AutoMapper;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class RealWorkoutProfile : Profile
{
    public RealWorkoutProfile()
    {
        CreateMap<RealWorkout, RealWorkoutFromCurrentWeekDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutFromCurrentWeekDto>();
        CreateMap<Instructor, InstructorForRealWorkoutFromCurrentWeekDto>();

        CreateMap<RealWorkout, RealWorkoutFromUpcomingWeekDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutFromUpcomingWeekDto>();
        CreateMap<Instructor, InstructorForRealWorkoutFromUpcomingWeekDto>();

        CreateMap<RealWorkout, RealWorkoutDetailDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutDetailDto>();
        CreateMap<Instructor, InstructorForRealWorkoutDetailDto>();
    }
}
