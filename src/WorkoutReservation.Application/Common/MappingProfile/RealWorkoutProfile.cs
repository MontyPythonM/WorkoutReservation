using AutoMapper;
using WorkoutReservation.Application.Common.Models;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class RealWorkoutProfile : Profile
    {
        public RealWorkoutProfile()
        {
            // RealWorkoutListDto
            CreateMap<RealWorkout, RealWorkoutListDto>();
        }
    }
}
