using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;

public record DeleteInstructorCommand(int InstructorId) : IRequest;

internal sealed class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly ILogger<DeleteInstructorCommandHandler> _logger;

    public DeleteInstructorCommandHandler(IInstructorRepository instructorRepository, 
        ILogger<DeleteInstructorCommandHandler> logger)
    {
        _instructorRepository = instructorRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteInstructorCommand request, CancellationToken token)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());

        await _instructorRepository.DeleteAsync(instructor, token);

        _logger.LogInformation($"Instructor with Id: {request.InstructorId} was deleted");
        return Unit.Value;
    }
}
