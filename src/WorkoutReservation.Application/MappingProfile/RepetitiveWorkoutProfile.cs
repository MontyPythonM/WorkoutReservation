using AutoMapper;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkouts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

public class RepetitiveWorkoutProfile : Profile
{
    public RepetitiveWorkoutProfile()
    {
        CreateMap<RepetitiveWorkout, RepetitiveWorkoutToRealWorkoutDto>();
    }
}
