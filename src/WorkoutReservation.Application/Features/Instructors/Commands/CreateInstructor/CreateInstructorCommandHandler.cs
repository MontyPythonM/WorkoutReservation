﻿using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;

public record CreateInstructorCommand(string FirstName, string LastName, Gender? Gender, 
    string Biography, string Email) : IRequest;

internal sealed class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand>
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IMapper _mapper;

    public CreateInstructorCommandHandler(IInstructorRepository instructorRepository, 
        IMapper mapper)
    {
        _instructorRepository = instructorRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(CreateInstructorCommand request, CancellationToken token)
    {
        var validator = new CreateInstructorCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var instructor = _mapper.Map<Instructor>(request);

        await _instructorRepository.AddAsync(instructor, token);
        return Unit.Value;
    }
}
