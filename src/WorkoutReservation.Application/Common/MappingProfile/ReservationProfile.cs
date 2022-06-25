using AutoMapper;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            // GetUserReservationsList
            CreateMap<Reservation, UserReservationsListDto>();
            CreateMap<RealWorkout, RealWorkoutForUserReservationsListDto>();
            CreateMap<WorkoutType, WorkoutTypeForUserReservationsListDto>();
            CreateMap<Instructor, InstructorForUserReservationsListDto>();

            // AddReservationCommand
            CreateMap<AddReservationCommand, Reservation>();
        }
    }
}
