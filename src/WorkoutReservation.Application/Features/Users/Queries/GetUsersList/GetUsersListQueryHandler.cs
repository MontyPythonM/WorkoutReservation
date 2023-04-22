using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Dtos;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

public record GetUsersListQuery : IRequest<PagedResultDto<UsersListDto>>, IPagedQuery
{
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal sealed class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery,
    PagedResultDto<UsersListDto>>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersListQueryHandler(IApplicationUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<UsersListDto>> Handle(GetUsersListQuery request, 
        CancellationToken token)
    {
        var validator = new GetUsersListQueryValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var pagedResult = await _userRepository.GetPagedAsync(request, token);
        var mappedResult = _mapper.Map<List<UsersListDto>>(pagedResult.users);

        return new PagedResultDto<UsersListDto>(mappedResult, pagedResult.totalItems, 
            request.PageSize, request.PageNumber);
    }
}
