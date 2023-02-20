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
        CreateMap<Reservation, UserReservationsListDto>()
            .ForMember(src => src.RealWorkoutId, dest => dest.MapFrom(r => r.RealWorkout.Id))
            .ForMember(src => src.MaxParticipantNumber, dest => dest.MapFrom(r => r.RealWorkout.MaxParticipantNumber))
            .ForMember(src => src.CurrentParticipantNumber,
                dest => dest.MapFrom(r => r.RealWorkout.CurrentParticipantNumber))
            .ForMember(src => src.StartTime, dest => dest.MapFrom(r => r.RealWorkout.StartTime))
            .ForMember(src => src.EndTime, dest => dest.MapFrom(r => r.RealWorkout.EndTime))
            .ForMember(src => src.Date, dest => dest.MapFrom(r => r.RealWorkout.Date))
            .ForMember(src => src.WorkoutTypeId, dest => dest.MapFrom(r => r.RealWorkout.WorkoutTypeId))
            .ForMember(src => src.WorkoutTypeName, dest => dest.MapFrom(r => r.RealWorkout.WorkoutType.Name))
            .ForMember(src => src.Intensity, dest => dest.MapFrom(r => r.RealWorkout.WorkoutType.Intensity))
            .ForMember(src => src.InstructorId, dest => dest.MapFrom(r => r.RealWorkout.InstructorId))
            .ForMember(src => src.InstructorFullName,
                dest => dest.MapFrom(r => string.Join(" ", r.RealWorkout.Instructor.FirstName, r.RealWorkout.Instructor.LastName)));

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
