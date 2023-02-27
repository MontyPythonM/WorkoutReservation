using AutoMapper;
using WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, UserReservationsListDto>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.RealWorkout.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.RealWorkout.EndTime))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.RealWorkout.Date))
            .ForMember(dest => dest.WorkoutTypeName, opt => opt.MapFrom(src => src.RealWorkout.WorkoutType.Name))
            .ForMember(dest => dest.InstructorFullName, opt => opt.MapFrom(src => 
                string.Join(" ", src.RealWorkout.Instructor.FirstName, src.RealWorkout.Instructor.LastName)));
        
        CreateMap<Reservation, ReservationDetailsDto>()
            .ForMember(dest => dest.RealWorkoutId, opt => opt.MapFrom(src => src.RealWorkout.Id))
            .ForMember(dest => dest.MaxParticipantNumber, opt => opt.MapFrom(src => src.RealWorkout.MaxParticipantNumber))
            .ForMember(dest => dest.CurrentParticipantNumber, opt => opt.MapFrom(src => src.RealWorkout.CurrentParticipantNumber))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.RealWorkout.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.RealWorkout.EndTime))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.RealWorkout.Date))
            .ForMember(dest => dest.WorkoutTypeId, opt => opt.MapFrom(src => src.RealWorkout.WorkoutTypeId))
            .ForMember(dest => dest.WorkoutTypeName, opt => opt.MapFrom(src => src.RealWorkout.WorkoutType.Name))
            .ForMember(dest => dest.Intensity, opt => opt.MapFrom(src => src.RealWorkout.WorkoutType.Intensity))
            .ForMember(dest => dest.InstructorId, opt => opt.MapFrom(src => src.RealWorkout.InstructorId))
            .ForMember(dest => dest.InstructorFullName, opt => opt.MapFrom(r =>
                string.Join(" ", r.RealWorkout.Instructor.FirstName, r.RealWorkout.Instructor.LastName)))
            .ForMember(dest => dest.IsWorkoutExpired, opt => opt.MapFrom((src, dest) => 
                src.RealWorkout.Date <= DateOnly.FromDateTime(DateTime.Now.Date) && src.RealWorkout.EndTime < TimeOnly.FromDateTime(DateTime.Now)));
    }
}
