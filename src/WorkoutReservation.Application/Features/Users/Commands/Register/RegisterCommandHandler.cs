using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IApplicationUserRepository _userRepository; 
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IApplicationUserRepository userRepository, 
        IPasswordHasher<ApplicationUser> passwordHasher, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken token)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, token);
        
        var validator = new RegisterCommandValidator(user);
        await validator.ValidateAndThrowAsync(request, token);

        var newUser = new ApplicationUser(request.Email, request.FirstName, 
            request.LastName, request.Gender, request.DateOfBirth, "");
        
        //var mappedUser = _mapper.Map<ApplicationUser>(request);
        var hashPassword = _passwordHasher.HashPassword(newUser, request.Password);
        
        newUser.SetPasswordHash(hashPassword);
        
        //TODO: dont work
        //newUser.SetRole(ApplicationRole.Member);
        
        await _userRepository.AddAsync(newUser, token);
        return Unit.Value;
    }
}
