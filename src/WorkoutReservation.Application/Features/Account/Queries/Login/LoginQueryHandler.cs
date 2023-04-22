using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Account.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<string>;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IApplicationUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    
    public LoginQueryHandler(IApplicationUserRepository userRepository, 
        IPasswordHasher<ApplicationUser> passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken token)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, true, token);

        if (user is null)
            throw new InvalidCredentialsException();

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        return await _jwtProvider.GenerateAsync(user, token);
    }
}