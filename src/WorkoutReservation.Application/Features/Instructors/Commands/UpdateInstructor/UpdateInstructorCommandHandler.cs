using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor
{
    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public UpdateInstructorCommandHandler(IInstructorRepository instructorRepository, 
                                              IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateInstructorCommand request, 
                                       CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId);

            if (instructor is null)
                throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

            var validator = new UpdateInstructorCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                throw new BadRequestException($"Validation error:\n{validatorResult}");

            var mappedWorkoutType = _mapper.Map<Instructor>(request);

            await _instructorRepository.UpdateAsync(mappedWorkoutType);

            return Unit.Value;

        }
    }
}
