using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor
{
    public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand, int>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public CreateInstructorCommandHandler(IInstructorRepository instructorRepository, 
                                              IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateInstructorCommand request, 
                                      CancellationToken cancellationToken)
        {
            var validator = new CreateInstructorCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if(!validatorResult.IsValid)
                throw new BadRequestException($"Validation error:\n{validatorResult}");

            var instructor = _mapper.Map<Instructor>(request);

            instructor = await _instructorRepository.AddAsync(instructor);

            return instructor.Id;
        }
    }
}
