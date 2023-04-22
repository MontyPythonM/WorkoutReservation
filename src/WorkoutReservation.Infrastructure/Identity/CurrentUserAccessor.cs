using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Authentication;
using WorkoutReservation.Shared.Exceptions;
using WorkoutReservation.Shared.Extensions;

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

    public async Task<ApplicationUser> GetUserAsync(CancellationToken token)
    {
        var user = await _userRepository.GetByGuidAsync(GetUserId(), false, token);
        
        if (user is null)
            throw new NotFoundException("Application user not exist");
        
        return user;
    }
    
    public Guid? GetNullableUserId() => _httpContextAccessor.HttpContext?.User
        .FindFirstValue(ClaimTypes.NameIdentifier).ToNullableGuid();

    public Guid GetUserId() => _httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier).ToGuid();

    public IEnumerable<Claim> GetUserClaims() => _httpContextAccessor.HttpContext!.User.Claims.ToList();

    public HashSet<string> GetUserPermissions() => GetUserClaims()
        .Where(claim => claim.Type == CustomClaims.Permissions)
        .Select(claim => claim.Value)
        .ToHashSet();

    public bool IsUserContextExist() => GetNullableUserId() is not null;
}