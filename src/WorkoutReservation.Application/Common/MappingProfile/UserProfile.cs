using AutoMapper;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Application.Features.Users.Queries.Login;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // register
        CreateMap<RegisterCommand, User>();

        // login
        CreateMap<LoginQuery, User>();

        // GetUsersList
        CreateMap<User, UsersListDto>();
    }
}
