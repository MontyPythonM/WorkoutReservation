using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;


namespace WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;

public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly ILogger<DeleteInstructorCommandHandler> _logger;

    public DeleteInstructorCommandHandler(IInstructorRepository instructorRepository, 
                                          ILogger<DeleteInstructorCommandHandler> logger)
    {
        _instructorRepository = instructorRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteInstructorCommand request, 
                                   CancellationToken cancellationToken)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, cancellationToken);

        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

        await _instructorRepository.DeleteAsync(instructor, cancellationToken);

        _logger.LogInformation($"Instructor with Id: {request.InstructorId} was deleted.");

        return Unit.Value;
    }
}
