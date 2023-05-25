using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public PasswordManager(IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string hashedPassword) 
        => _passwordHasher.VerifyHashedPassword(default, hashedPassword, password) == 
           PasswordVerificationResult.Success;
}