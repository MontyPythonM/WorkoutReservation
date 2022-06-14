using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository; 
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUserRepository userRepository, 
                                      IPasswordHasher<User> passwordHasher, 
                                      IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterCommand request, 
                                       CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            var validator = new RegisterCommandValidator(user);
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var mappedUser = _mapper.Map<User>(request);

            var hashPassword = _passwordHasher.HashPassword(mappedUser, request.Password);

            mappedUser.PasswordHash = hashPassword;
            mappedUser.AccountCreationDate = DateTime.Now;
            mappedUser.UserRole = UserRole.Member;

            await _userRepository.AddUser(mappedUser);

            return Unit.Value;
        }
    }
}
