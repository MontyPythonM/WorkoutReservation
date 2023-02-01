using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;

public record UpdateInstructorCommand(int Id, string FirstName, string LastName, Gender? Gender, 
    string Biography, string Email) : IRequest;

internal sealed class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand>
{
    private readonly IInstructorRepository _instructorRepository;

    public UpdateInstructorCommandHandler(IInstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<Unit> Handle(UpdateInstructorCommand request, CancellationToken token)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.Id, false, token);

        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.Id} not found.");

        var validator = new UpdateInstructorCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);

        instructor.FirstName = request.FirstName;
        instructor.LastName = request.LastName;
        instructor.Biography = request.Biography;
        instructor.Email = request.Email;
        instructor.Gender = request.Gender;
        
        await _instructorRepository.UpdateAsync(instructor, token);
        return Unit.Value;
    }
}
