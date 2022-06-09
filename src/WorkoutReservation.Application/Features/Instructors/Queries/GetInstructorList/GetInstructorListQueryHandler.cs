using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList
{
    public class GetInstructorListQueryHandler : IRequestHandler<GetInstructorListQuery, 
                                                                 List<InstructorListQueryDto>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public GetInstructorListQueryHandler(IInstructorRepository instructorRepository, 
                                             IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<List<InstructorListQueryDto>> Handle(GetInstructorListQuery request, 
                                                               CancellationToken cancellationToken)
        {
            var instructors = await _instructorRepository.GetAllAsync();

            if(!instructors.Any())
                throw new NotFoundException($"Instructors not found.");

            return _mapper.Map<List<InstructorListQueryDto>>(instructors);
        }

    }
}
