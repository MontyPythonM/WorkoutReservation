using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;

public record GetInstructorDetailQuery(int InstructorId) : IRequest<InstructorDetailQueryDto>;

internal sealed class GetInstructorDetailQueryHandler : IRequestHandler<GetInstructorDetailQuery, 
    InstructorDetailQueryDto>
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IMapper _mapper;

    public GetInstructorDetailQueryHandler(IInstructorRepository instructorRepository,
        IMapper mapper)
    {
        _instructorRepository = instructorRepository;
        _mapper = mapper;
    }

    public async Task<InstructorDetailQueryDto> Handle(GetInstructorDetailQuery request, 
        CancellationToken token)
    {
        var instructor = await _instructorRepository
            .GetByIdAsync(request.InstructorId, true, token, incl => incl.WorkoutTypes);

        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());

        return _mapper.Map<InstructorDetailQueryDto>(instructor);
    }
}
