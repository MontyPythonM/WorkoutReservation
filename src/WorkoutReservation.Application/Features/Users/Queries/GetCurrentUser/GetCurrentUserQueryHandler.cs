using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<CurrentUserDto>;

internal sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IMapper _mapper;
    
    public GetCurrentUserQueryHandler(ICurrentUserAccessor currentUserAccessor, IMapper mapper)
    {
        _currentUserAccessor = currentUserAccessor;
        _mapper = mapper;
    }
    
    public async Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);
        return _mapper.Map<CurrentUserDto>(user);
    }
}