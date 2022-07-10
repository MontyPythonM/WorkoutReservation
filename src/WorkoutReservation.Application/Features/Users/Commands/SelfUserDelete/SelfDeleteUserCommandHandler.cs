using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;

public class SelfDeleteUserCommandHandler : IRequestHandler<SelfDeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public SelfDeleteUserCommandHandler(IUserRepository userRepository,
                                        ICurrentUserService currentUserService,
                                        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(SelfDeleteUserCommand request, 
                                   CancellationToken cancellationToken)
    {
        var currentUserGuid = Guid.Parse(_currentUserService.UserId);

        var user = await _userRepository.GetByGuidAsync(currentUserGuid);

        var passwordCompareResult = _passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, request.Password);

        var validator = new SelfDeleteUserCommandValidator(passwordCompareResult);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        await _userRepository.DeleteAsync(user);

        return Unit.Value;
    }
}
