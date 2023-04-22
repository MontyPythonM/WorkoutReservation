using AutoMapper;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

public class RealWorkoutProfile : Profile
{
    public RealWorkoutProfile()
    {
        CreateMap<RealWorkout, RealWorkoutDetailDto>();
        CreateMap<WorkoutType, WorkoutTypeForRealWorkoutDetailDto>();
        CreateMap<Instructor, InstructorForRealWorkoutDetailDto>();
    }
}
