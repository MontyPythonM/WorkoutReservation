﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Account.Commands.SelfUserDelete;

public record SelfDeleteUserCommand(string Password) : IRequest;

internal sealed class SelfDeleteUserCommandHandler : IRequestHandler<SelfDeleteUserCommand>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public SelfDeleteUserCommandHandler(IApplicationUserRepository userRepository,
        ICurrentUserAccessor currentUserAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(SelfDeleteUserCommand request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);

        var passwordCompareResult = _passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, request.Password);

        var validator = new SelfDeleteUserCommandValidator(passwordCompareResult);
        await validator.ValidateAndThrowAsync(request, token);

        await _userRepository.DeleteAsync(user, token);
        return Unit.Value;
    }
}