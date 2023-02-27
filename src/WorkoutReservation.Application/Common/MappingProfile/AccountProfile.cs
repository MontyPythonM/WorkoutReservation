using AutoMapper;
using WorkoutReservation.Application.Features.Account.Queries.GetCurrentUser;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<ApplicationUser, CurrentUserDto>()
            .ForMember(desc => desc.Roles, opt => opt.MapFrom(src => src.ApplicationRoles.Select(role => role.Name)))
            .ForMember(desc => desc.Permissions, opt => opt.MapFrom(src => src.ApplicationRoles
                .SelectMany(role => role.ApplicationPermissions.Select(permission => permission.Name))));
    }
}