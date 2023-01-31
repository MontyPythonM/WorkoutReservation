using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery,
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

        var usersQuery = _userRepository.GetAllUsersQuery();

        var query = usersQuery
            .Where(x => request.SearchPhrase == null ||
                   x.Email.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<ApplicationUser, object>>>
            {
                { nameof(ApplicationUser.Email), u => u.Email},
                { nameof(ApplicationUser.FirstName), u => u.FirstName},
                { nameof(ApplicationUser.LastName), u => u.LastName},
            };

            var sortByExpression = columnsSelector[request.SortBy];

            query = request.SortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        var result = query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToList();

        var mappedResult = _mapper.Map<List<UsersListDto>>(result);

        return new PagedResultDto<UsersListDto>(mappedResult, totalCount, 
            request.PageSize, request.PageNumber);
    }
}
