using AutoMapper;
using WorkoutReservation.Application.Common.Models;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Domain.Entities;

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

            // CreateRealWorkoutCommandHander
            CreateMap<CreateRealWorkoutCommand, RealWorkout>();

            // UpdateRealWorkoutCommand
            CreateMap<UpdateRealWorkoutCommand, RealWorkout>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.RealWorkoutId)); ;
        }
    }
}
