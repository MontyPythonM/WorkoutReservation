﻿using AutoMapper;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UsersListDto>()
            .ForMember(desc => desc.UserRoles, opt => opt.MapFrom(src => src.ApplicationRoles.Select(role => role.Name)));
    }
}
