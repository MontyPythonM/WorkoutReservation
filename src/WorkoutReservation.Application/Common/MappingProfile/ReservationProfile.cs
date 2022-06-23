using AutoMapper;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            // AddReservationCommand
            CreateMap<AddReservationCommand, Reservation>();
        }

    }
}
