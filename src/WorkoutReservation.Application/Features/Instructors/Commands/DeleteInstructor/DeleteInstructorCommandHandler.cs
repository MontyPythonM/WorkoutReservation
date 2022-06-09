using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor
{
    public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepository;

        public DeleteInstructorCommandHandler(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<Unit> Handle(DeleteInstructorCommand request, 
                                       CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId);

            if (instructor is null)
                throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

            await _instructorRepository.DeleteAsync(instructor);

            return Unit.Value;
        }
    }
}
