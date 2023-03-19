using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WorkoutReservation.Infrastructure.Interfaces;

namespace WorkoutReservation.Infrastructure.Identity;

public class AuthorProvider : IAuthorProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetAuthor() => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}