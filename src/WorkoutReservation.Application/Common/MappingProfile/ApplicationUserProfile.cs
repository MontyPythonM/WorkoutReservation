using AutoMapper;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UsersListDto>()
            .ForMember(desc => desc.UserRoles, src => src.MapFrom(user => user.ApplicationRoles.Select(role => role.Name)));
    }
}
