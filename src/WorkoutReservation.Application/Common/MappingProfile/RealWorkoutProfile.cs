﻿using AutoMapper;
using WorkoutReservation.Application.Common.Models;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class RealWorkoutProfile : Profile
    {
        public RealWorkoutProfile()
        {
            // GetRealWorkoutFromCurrentWeekQuery
            CreateMap<RealWorkout, RealWorkoutListDto>();

            // GetRealWorkoutDetailQueryHandler
            CreateMap<RealWorkout, RealWorkoutDetailDto>();
        }
    }
}