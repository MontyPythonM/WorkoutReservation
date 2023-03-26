using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public record GetReservationsQuery : IRequest<PagedResultDto<ReservationListDto>>, IPagedQuery
{
    public string SearchPhrase { get; set; }
    public string SortBy { get; set; }
    public bool SortByDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetReservationsHandler : IRequestHandler<GetReservationsQuery, 
    PagedResultDto<ReservationListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetReservationsHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<ReservationListDto>> Handle(GetReservationsQuery request, 
        CancellationToken token)
    {
        var validator = new GetReservationsValidator();
        await validator.ValidateAndThrowAsync(request, token);

        var reservationsQuery = _reservationRepository.GetReservationsQuery();

        var query = reservationsQuery
            .Where(x => request.SearchPhrase == null ||
                        x.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.Id.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.User.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.WorkoutType.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                        x.RealWorkout.Instructor.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { SortBySelector.ReservationId.StringValue(), u => u.Id },
                { SortBySelector.ReservationStatus.StringValue(), u => u.ReservationStatus },
                { SortBySelector.CreatedDate.StringValue(), u => u.CreatedDate },
                { SortBySelector.LastModifiedDate.StringValue(), u => u.LastModifiedDate },
                { SortBySelector.WorkoutDate.StringValue(), u => u.RealWorkout.Date },
                { SortBySelector.WorkoutName.StringValue(), u => u.RealWorkout.WorkoutType.Name }
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

        var mappedResult = _mapper.Map<List<ReservationListDto>>(result);

        return new PagedResultDto<ReservationListDto>(mappedResult,
            totalCount, request.PageSize, request.PageNumber);
    }
}