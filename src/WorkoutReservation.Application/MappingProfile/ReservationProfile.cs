using AutoMapper;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

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
    }
}
