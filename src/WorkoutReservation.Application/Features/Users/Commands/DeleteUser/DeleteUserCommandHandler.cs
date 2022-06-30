using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository,
                                        ICurrentUserService currentUserService,
                                        ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, 
                                       CancellationToken cancellationToken)
        {
            var userToRemove = await _userRepository.GetByGuidAsync(request.UserGuid);

            if (userToRemove is null)
                throw new NotFoundException($"User with Guid: {request.UserGuid} not found.");

            var currentUserGuid = Guid.Parse(_currentUserService.UserId);

            var validator = new DeleteUserCommandValidator(currentUserGuid);
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            await _userRepository.DeleteAsync(userToRemove);

            _logger.LogInformation($"User with Id: {request.UserGuid} was deleted by Administrator Id: {currentUserGuid}.");

            return Unit.Value;
        }
    }
}
