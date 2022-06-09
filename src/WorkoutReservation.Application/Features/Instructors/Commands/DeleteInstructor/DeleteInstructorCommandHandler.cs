using MediatR;
using NLog;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor
{
    public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ILogger _logger;

        public DeleteInstructorCommandHandler(IInstructorRepository instructorRepository, ILogger logger)
        {
            _instructorRepository = instructorRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteInstructorCommand request, 
                                       CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId);

            if (instructor is null)
                throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

            await _instructorRepository.DeleteAsync(instructor);

            _logger.Info($"Instructor with Id: {request.InstructorId} was deleted.");

            return Unit.Value;
        }
    }
}
