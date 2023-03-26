using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;

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

        var pagedResult = await _reservationRepository.GetPagedAsync(request, token);
        var mappedResult = _mapper.Map<List<ReservationListDto>>(pagedResult.reservations);

        return new PagedResultDto<ReservationListDto>(mappedResult,
            pagedResult.totalItems, request.PageSize, request.PageNumber);
    }
}