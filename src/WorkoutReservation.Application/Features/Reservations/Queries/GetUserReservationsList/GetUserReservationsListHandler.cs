using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

public record GetUserReservationsListQuery : IRequest<PagedResultDto<UserReservationsListDto>>, IPagedQuery
{
    public Guid UserId { get; set; }
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal sealed class GetUserReservationsListHandler : IRequestHandler<GetUserReservationsListQuery,
    PagedResultDto<UserReservationsListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetUserReservationsListHandler(IReservationRepository reservationRepository, 
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<UserReservationsListDto>> Handle(GetUserReservationsListQuery request, 
        CancellationToken token)
    {
        var validator = new GetUserReservationsListValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        var reservationsQuery = _reservationRepository
            .GetUserReservationsQuery(request.UserId);

        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                   x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                   x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()));
        
        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { nameof(Reservation.ReservationStatus), u => u.ReservationStatus},
                { "WorkoutDate", u => u.RealWorkout.Date}
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

        var mappedResult = _mapper.Map<List<UserReservationsListDto>>(result);

        return new PagedResultDto<UserReservationsListDto>(mappedResult,
            totalCount, request.PageSize, request.PageNumber);
    }
}
