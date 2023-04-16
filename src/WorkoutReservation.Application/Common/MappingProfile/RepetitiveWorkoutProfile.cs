using AutoMapper;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class RepetitiveWorkoutProfile : Profile
{
    public RepetitiveWorkoutProfile()
    {
        CreateMap<RepetitiveWorkout, RepetitiveWorkoutToRealWorkoutDto>();
    }
}
