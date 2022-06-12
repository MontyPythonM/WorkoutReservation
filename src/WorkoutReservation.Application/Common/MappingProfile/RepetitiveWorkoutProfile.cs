using AutoMapper;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class RepetitiveWorkoutProfile : Profile
    {
        public RepetitiveWorkoutProfile()
        {
            // RepetitiveWorkoutListDto
            CreateMap<RepetitiveWorkout, RepetitiveWorkoutListDto>();

            // CreateInstructorCommandHandler
            CreateMap<CreateRepetitiveWorkoutCommand, RepetitiveWorkout>();

            // UpdateInstructorCommandHandler
            CreateMap<UpdateRepetitiveWorkoutCommand, RepetitiveWorkout>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.RepetitiveWorkoutId));
        }
    }
}
