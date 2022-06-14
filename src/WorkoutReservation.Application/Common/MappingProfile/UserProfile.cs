using AutoMapper;
using WorkoutReservation.Application.Features.Users.Commands.Login;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // register
            CreateMap<RegisterCommand, User>()
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email));

            // login
            CreateMap<LoginCommand, User>();
        }

    }
}
