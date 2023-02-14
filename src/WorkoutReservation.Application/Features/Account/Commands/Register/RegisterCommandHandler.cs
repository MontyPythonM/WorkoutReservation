using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Account.Commands.Register;

public record RegisterCommand(string Email, string Password, string ConfirmPassword, string FirstName, 
    string LastName, Gender? Gender, DateOnly? DateOfBirth) : IRequest;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IApplicationUserRepository _userRepository; 
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IApplicationRoleRepository _roleRepository;

    public RegisterCommandHandler(IApplicationUserRepository userRepository, 
        IPasswordHasher<ApplicationUser> passwordHasher, 
        IApplicationRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken token)
    {
        var user = await _userRepository
            .GetByEmailAsync(request.Email, false, token, incl => incl.ApplicationRoles);
        
        var validator = new RegisterCommandValidator(user);
        await validator.ValidateAndThrowAsync(request, token);

        var newUser = new ApplicationUser(request.Email, request.FirstName, 
            request.LastName, request.Gender, request.DateOfBirth, "");
        
        var hashPassword = _passwordHasher.HashPassword(newUser, request.Password);
        newUser.SetPasswordHash(hashPassword);

        var role = await _roleRepository.GetAsync(Role.Member, token);
        newUser.SetRole(role);
        
        await _userRepository.AddAsync(newUser, token);
        return Unit.Value;
    }
}
