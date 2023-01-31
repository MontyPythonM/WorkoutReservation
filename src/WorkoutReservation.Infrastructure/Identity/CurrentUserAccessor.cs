using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Infrastructure.Identity;

public sealed class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationUserRepository _userRepository;
    
    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor, 
        IApplicationUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync(CancellationToken token)
    {
        var user = await _userRepository.GetByGuidAsync(GetCurrentUserId(), false, token);
        
        if (user is null)
            throw new NotFoundException("Application user not exist");
        
        return user;
    }
    
    public Guid GetCurrentUserId() => _httpContextAccessor.HttpContext?.User
        .FindFirstValue(ClaimTypes.NameIdentifier)
        .ToGuid() ?? throw new NotFoundException("Current user Id not exist");
}