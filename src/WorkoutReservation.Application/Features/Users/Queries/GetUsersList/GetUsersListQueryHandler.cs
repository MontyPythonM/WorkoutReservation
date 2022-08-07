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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersListQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<UsersListDto>> Handle(GetUsersListQuery request, 
                                                           CancellationToken cancellationToken)
    {
        var validator = new GetUsersListQueryValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var usersQuery = _userRepository.GetAllUsersQuery();

        var query = usersQuery
            .Where(x => request.SearchPhrase == null ||
                   x.Email.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
            {
                { nameof(User.Email), u => u.Email},
                { nameof(User.FirstName), u => u.FirstName},
                { nameof(User.LastName), u => u.LastName},
                { nameof(User.UserRole), u => u.UserRole},
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

        var pagedWorkoutTypes = new PagedResultDto<UsersListDto>(mappedResult,
                                                                 totalCount,
                                                                 request.PageSize,
                                                                 request.PageNumber);
        return pagedWorkoutTypes;
    }
}
