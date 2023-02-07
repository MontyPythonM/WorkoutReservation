using AutoMapper;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Application.Features.Users.Queries.GetCurrentUser;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Application.Features.Users.Queries.Login;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UsersListDto>()
            .ForMember(desc => desc.UserRoles, src => src.MapFrom(user => user.ApplicationRoles.Select(role => role.Name)));

        CreateMap<ApplicationUser, CurrentUserDto>()
            .ForMember(desc => desc.Roles, src => src.MapFrom(user => user.ApplicationRoles.Select(role => role.Name)))
            .ForMember(desc => desc.Permissions, src => src.MapFrom(user => user.ApplicationRoles
                .SelectMany(role => role.ApplicationPermissions.Select(permission => permission.Name))));
    }
}
