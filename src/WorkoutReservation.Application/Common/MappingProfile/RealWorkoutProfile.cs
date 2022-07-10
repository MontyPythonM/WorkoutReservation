using AutoMapper;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class RealWorkoutProfile : Profile
{
    public RealWorkoutProfile()
    {
        // GetRealWorkoutFromCurrentWeekQuery
        CreateMap<RealWorkout, RealWorkoutFromCurrentWeekDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutFromCurrentWeekDto>();
        CreateMap<Instructor, InstructorForRealWorkoutFromCurrentWeekDto>();

        // GetRealWorkoutFromUpcomingWeekQuery
        CreateMap<RealWorkout, RealWorkoutFromUpcomingWeekDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutFromUpcomingWeekDto>();
        CreateMap<Instructor, InstructorForRealWorkoutFromUpcomingWeekDto>();

        // GetRealWorkoutDetailQuery
        CreateMap<RealWorkout, RealWorkoutDetailDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutDetailDto>();
        CreateMap<Instructor, InstructorForRealWorkoutDetailDto>();

        // CreateRealWorkoutCommand
        CreateMap<CreateRealWorkoutCommand, RealWorkout>();

        // UpdateRealWorkoutCommand
        CreateMap<UpdateRealWorkoutCommand, RealWorkout>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.RealWorkoutId)); ;
    }
}
