using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.Account.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<string>;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtProvider _jwtProvider;
    
    public LoginQueryHandler(IApplicationUserRepository userRepository, 
        IPasswordManager passwordManager, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken token)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, true, token);

        if (user is null)
            throw new InvalidCredentialsException();

        var isPasswordValid = _passwordManager.Validate(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new InvalidCredentialsException();

        return await _jwtProvider.GenerateAsync(user, token);
    }
}