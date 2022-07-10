using AutoMapper;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

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

        // CancelReservationCommand
        CreateMap<CancelReservationCommand, Reservation>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.ReservationId));

        // EditReservationStatusCommand
        CreateMap<EditReservationStatusCommand, Reservation>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.ReservationId));
    }
}
