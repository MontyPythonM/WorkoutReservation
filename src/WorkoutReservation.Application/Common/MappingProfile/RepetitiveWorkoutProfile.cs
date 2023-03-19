using AutoMapper;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class RepetitiveWorkoutProfile : Profile
{
    public RepetitiveWorkoutProfile()
    {
        CreateMap<RepetitiveWorkout, RepetitiveWorkoutListDto>();
        CreateMap<WorkoutType, WorkoutTypeForRepetitiveWorkoutListDto>();
        CreateMap<Instructor, InstructorForRepetitiveWorkoutListDto>();
        
        CreateMap<RepetitiveWorkout, RepetitiveWorkoutToRealWorkoutDto>();
    }
}
