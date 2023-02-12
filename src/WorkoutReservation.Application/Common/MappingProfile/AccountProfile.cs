using AutoMapper;
using WorkoutReservation.Application.Features.Account.Queries.GetCurrentUser;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<ApplicationUser, CurrentUserDto>()
            .ForMember(desc => desc.Roles, src => src.MapFrom(user => user.ApplicationRoles.Select(role => role.Name)))
            .ForMember(desc => desc.Permissions, src => src.MapFrom(user => user.ApplicationRoles
                .SelectMany(role => role.ApplicationPermissions.Select(permission => permission.Name))));
    }
}