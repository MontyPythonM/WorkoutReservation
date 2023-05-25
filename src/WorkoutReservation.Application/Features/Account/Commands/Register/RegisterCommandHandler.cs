using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Account.Commands.Register;

public record RegisterCommand(string Email, string Password, string ConfirmPassword, string FirstName, 
    string LastName, Gender? Gender, DateOnly? DateOfBirth) : IRequest;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IApplicationUserRepository _userRepository; 
    private readonly IPasswordManager _passwordManager;
    private readonly IApplicationRoleRepository _roleRepository;
    
    public RegisterCommandHandler(IApplicationUserRepository userRepository, 
        IPasswordManager passwordManager, 
        IApplicationRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _roleRepository = roleRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken token)
    {
        var isEmailAlreadyTaken = await _userRepository.IsEmailAlreadyTaken(request.Email, token);
        
        var validator = new RegisterCommandValidator(isEmailAlreadyTaken);
        await validator.ValidateAndThrowAsync(request, token);

        var newUser = new ApplicationUser(request.Email, request.FirstName, request.LastName, 
            request.Gender, request.DateOfBirth, _passwordManager.Secure(request.Password));

        newUser.SetRole(await _roleRepository.GetAsync(Role.Member, token));
        
        await _userRepository.AddAsync(newUser, token);
        return Unit.Value;
    }
}
