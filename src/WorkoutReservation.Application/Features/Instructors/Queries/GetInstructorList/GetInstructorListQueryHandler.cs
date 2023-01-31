using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

public record GetInstructorListQuery() : IRequest<List<InstructorListQueryDto>>;

internal sealed class GetInstructorListQueryHandler : IRequestHandler<GetInstructorListQuery, 
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
        CancellationToken token)
    {
        var instructors = await _instructorRepository.GetAllAsync(true, token);

        if(!instructors.Any())
            throw new NotFoundException($"Instructors not found.");

        return _mapper.Map<List<InstructorListQueryDto>>(instructors);
    }

}
